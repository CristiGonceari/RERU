using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.GetEvaluationQuestion
{
    public class GetEvaluationQuestionQueryHandler : IRequestHandler<GetEvaluationQuestionQuery, VerificationTestQuestionUnitDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IQuestionUnitService _questionUnitService;

        public GetEvaluationQuestionQueryHandler(AppDbContext appDbContext, IMapper mapper,
            IQuestionUnitService questionUnitService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _questionUnitService = questionUnitService;
        }

        public async Task<VerificationTestQuestionUnitDto> Handle(GetEvaluationQuestionQuery request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.TestQuestions)
                    .ThenInclude(t => t.QuestionUnit)
                        .ThenInclude(x => x.Options)
                .FirstAsync(x => x.Id == request.TestId);

            var testQuestion = test.TestQuestions.FirstOrDefault(x => x.Index == request.QuestionIndex);

            if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                testQuestion.QuestionUnit = await _questionUnitService.GetHashedQuestionUnit(testQuestion.QuestionUnit.Id);
            }

            var answer = _mapper.Map<VerificationTestQuestionUnitDto>(testQuestion);

            if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                answer.CorrectHashedQuestion = await _questionUnitService.GetUnHashedQuestionWithoutTags(testQuestion.QuestionUnit.Id);
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
                        if (testQuestion.QuestionUnit.QuestionType != QuestionTypeEnum.HashedAnswer)
                        {
                            var option = answer.Options.FirstOrDefault(x => x.Id == savedAnswer.OptionId);
                            option.IsSelected = true;

                            if (!string.IsNullOrEmpty(savedAnswer.AnswerValue))
                            {
                                option.Answer = savedAnswer.AnswerValue;
                            }
                        }
                        else
                        {
                            answer.Question = answer.Question.Replace($"optionId='{savedAnswer.OptionId}'>",
                                $"optionId='{savedAnswer.OptionId}'>{savedAnswer.AnswerValue}");
                            answer.HashedOptions.FirstOrDefault(x => x.Id == savedAnswer.OptionId).Answer = savedAnswer.AnswerValue;
                        }
                    }
                }
            }

            return answer;
        }
    }
}
