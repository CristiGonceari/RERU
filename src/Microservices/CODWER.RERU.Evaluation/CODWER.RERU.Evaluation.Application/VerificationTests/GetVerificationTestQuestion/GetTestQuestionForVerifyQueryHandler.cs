using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.GetVerificationTestQuestion
{
    public class GetTestQuestionForVerifyQueryHandler : IRequestHandler<GetTestQuestionForVerifyQuery, VerificationTestQuestionUnitDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IQuestionUnitService _questionUnitService;

        public GetTestQuestionForVerifyQueryHandler(AppDbContext appDbContext, IMapper mapper,
            IQuestionUnitService questionUnitService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _questionUnitService = questionUnitService;
        }

        public async Task<VerificationTestQuestionUnitDto> Handle(GetTestQuestionForVerifyQuery request,
            CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(t => t.TestQuestions)
                .ThenInclude(t => t.QuestionUnit)
                .ThenInclude(x => x.Options)
                .FirstAsync(x => x.Id == request.Data.TestId);

            var testQuestion = test.TestQuestions.FirstOrDefault(x => x.Index == request.Data.QuestionIndex);

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
                    answer.AnswerText = _appDbContext.TestAnswers.FirstOrDefault(x => x.TestQuestionId == testQuestion.Id).AnswerValue;
                }
                else
                {
                    var savedAnswers = _appDbContext.TestAnswers
                        .Where(x => x.TestQuestionId == testQuestion.Id)
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
