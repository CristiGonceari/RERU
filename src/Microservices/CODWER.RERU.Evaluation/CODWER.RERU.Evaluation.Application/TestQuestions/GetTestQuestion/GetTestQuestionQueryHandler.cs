﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestion
{
    public class GetTestQuestionQueryHandler : IRequestHandler<GetTestQuestionQuery, TestQuestionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IQuestionUnitService _questionUnitService;

        public GetTestQuestionQueryHandler(AppDbContext appDbContext, IMapper mapper, IQuestionUnitService questionUnitService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _questionUnitService = questionUnitService;
        }

        public async Task<TestQuestionDto> Handle(GetTestQuestionQuery request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(t => t.TestQuestions)
                    .ThenInclude(t => t.QuestionUnit)
                        .ThenInclude(q => q.QuestionCategory)
                            .ThenInclude(x => x.QuestionUnits)
                                .ThenInclude(q => q.Options)
                .FirstAsync(x => x.Id == request.TestId);

            var testQuestion = test.TestQuestions.FirstOrDefault(x => x.Index == request.QuestionIndex);
            var questionUnit = testQuestion.QuestionUnit;

            if (questionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                questionUnit = await _questionUnitService.GetHashedQuestionUnit(questionUnit.Id);
            }

            var answer = _mapper.Map<TestQuestionDto>(questionUnit);
            answer.AnswerStatus = testQuestion.AnswerStatus;
            answer.TimeLimit = testQuestion.TimeLimit;

            if (testQuestion.AnswerStatus == AnswerStatusEnum.Answered)
            {
                if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.FreeText)
                {
                    answer.AnswerText = _appDbContext.TestAnswers.FirstOrDefault(x => x.TestQuestionId == testQuestion.Id).AnswerValue;
                }
                else
                {
                    var savedAnswers = _appDbContext.TestAnswers.Where(x => x.TestQuestionId == testQuestion.Id).ToList();

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
                        else
                        {
                            answer.Question = answer.Question.Replace($"optionId='{savedAnswer.OptionId}'>", $"optionId='{savedAnswer.OptionId}'>{savedAnswer.AnswerValue}");
                            answer.HashedOptions.FirstOrDefault(x => x.Id == savedAnswer.OptionId).Answer = savedAnswer.AnswerValue;
                        }
                    }
                }
            }

            return answer;
        }
    }
 }
