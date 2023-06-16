using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;
using System;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestion
{
    public class GetTestQuestionQueryHandler : IRequestHandler<GetTestQuestionQuery, TestQuestionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IQuestionUnitService _questionUnitService;
        private readonly Random _random;

        public GetTestQuestionQueryHandler(AppDbContext appDbContext, IMapper mapper, IQuestionUnitService questionUnitService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _questionUnitService = questionUnitService;
            _random = new Random();
        }

        public async Task<TestQuestionDto> Handle(GetTestQuestionQuery request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(t => t.TestQuestions)
                    .ThenInclude(t => t.QuestionUnit)
                        .ThenInclude(q => q.QuestionCategory)
                            .ThenInclude(x => x.QuestionUnits)
                                .ThenInclude(q => q.Options)
                .Include(t => t.TestQuestions)
                    .ThenInclude(x => x.TestQuestionsTestAnswers)
                        .ThenInclude(x => x.TestAnswer)
                .FirstAsync(x => x.Id == request.TestId);

            var testQuestion = test.TestQuestions.FirstOrDefault(x => x.Index == request.QuestionIndex);
            var questionUnit = testQuestion.QuestionUnit;

            if (questionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                questionUnit = await _questionUnitService.GetHashedQuestionUnit(questionUnit.Id);
            }

            var answer = _mapper.Map<TestQuestionDto>(questionUnit);
            answer.Id = testQuestion.Id;
            answer.AnswerStatus = testQuestion.AnswerStatus;
            answer.TimeLimit = testQuestion.TimeLimit;

            if (questionUnit.QuestionType == QuestionTypeEnum.MultipleAnswers || questionUnit.QuestionType == QuestionTypeEnum.OneAnswer)
            {
                var options = answer.Options.Where(x => x.QuestionUnitId == questionUnit.Id).OrderBy(opt => _random.Next()).ToList();
                answer.Options = options;
            }

            if (testQuestion.AnswerStatus == AnswerStatusEnum.Answered)
            {
                if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.FreeText)
                {
                    answer.AnswerText = _appDbContext.TestQuestionsTestAnswers
                        .Include(x => x.TestAnswer)
                        .FirstOrDefault(x => x.TestQuestionId == testQuestion.Id)?.TestAnswer.AnswerValue;
                }
                else
                {
                    var savedAnswers = _appDbContext.TestQuestionsTestAnswers
                        .Include(x => x.TestAnswer)
                        .Where(x => x.TestQuestionId == testQuestion.Id)
                        .Select(x => x.TestAnswer)
                        .ToList();

                    foreach (var savedAnswer in savedAnswers)
                    {
                        var option = answer.Options.FirstOrDefault(x => x.Id == savedAnswer.OptionId);

                        if (questionUnit.QuestionType != QuestionTypeEnum.HashedAnswer)
                        {
                            option.IsSelected = true;

                            if (!string.IsNullOrEmpty(savedAnswer.AnswerValue))
                            {
                                option.Answer = savedAnswer.AnswerValue;
                            }
                        }
                        
                        answer.Question = answer.Question.Replace($"optionId='{savedAnswer.OptionId}'>", $"optionId='{savedAnswer.OptionId}'>{savedAnswer.AnswerValue}");
                    }
                }
            }

            return answer;
        }
    }
 }
