using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

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

        public async Task<VerificationTestQuestionUnitDto> Handle(GetTestQuestionForVerifyQuery request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(t => t.TestTemplate)
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

            CalculateOptionsScoreByFormula(testQuestion, test.TestTemplate, answer);

            return answer;
        }

        private void CalculateOptionsScoreByFormula(TestQuestion testQuestion, TestTemplate testTemplate, VerificationTestQuestionUnitDto answer)
        {
            var correctOptions = testQuestion.QuestionUnit.Options.Where(x => x.IsCorrect).Count();

            foreach(var option in answer.Options)
            {
                var percentOfOneCorrectAnswer = (int)Math.Round((double)(100 / correctOptions));
                var percentOfOneInCorrectAnswer = 0;
                double pointOfOneCorrectAnswer = (double)answer.QuestionMaxPoints / correctOptions;
                double pointOfOneInCorrectAnswer = 0;


                if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer)
                {
                    if (testTemplate.Settings.FormulaForOneAnswer == ScoreFormulaEnum.MinusCorrectOption)
                    {
                        percentOfOneInCorrectAnswer = percentOfOneCorrectAnswer * (-1);
                        pointOfOneInCorrectAnswer = pointOfOneCorrectAnswer * (-1);
                    }
                    else if (testTemplate.Settings.FormulaForOneAnswer == ScoreFormulaEnum.OneDivideCountPercent)
                    {
                        percentOfOneInCorrectAnswer = (int)Math.Round(((double)1 / testQuestion.QuestionUnit.Options.Count()) * 100) * -1;
                        pointOfOneInCorrectAnswer = (double)answer.QuestionMaxPoints * percentOfOneInCorrectAnswer / 100;
                    }

                    if(testTemplate.Settings.NegativeScoreForOneAnswer == false)
                    {
                        answer.ShowNegativeMessage = true;
                    }
                }
                else if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.MultipleAnswers)
                {
                    if (testTemplate.Settings.FormulaForMultipleAnswers == ScoreFormulaEnum.MinusCorrectOption)
                    {
                        percentOfOneInCorrectAnswer = percentOfOneCorrectAnswer * (-1);
                        pointOfOneInCorrectAnswer = pointOfOneCorrectAnswer * (-1);
                    }
                    else if (testTemplate.Settings.FormulaForMultipleAnswers == ScoreFormulaEnum.OneDivideCountPercent)
                    {
                        percentOfOneInCorrectAnswer = (int)Math.Round((double)1 / testQuestion.QuestionUnit.Options.Count() * 100) * -1;
                        pointOfOneInCorrectAnswer = (double)answer.QuestionMaxPoints * percentOfOneInCorrectAnswer / 100;
                    }

                    if (testTemplate.Settings.NegativeScoreForMultipleAnswers == false)
                    {
                        answer.ShowNegativeMessage = true;
                    }
                }

                if (option.IsCorrect)
                {
                    option.Percentages = percentOfOneCorrectAnswer;
                    option.Points = String.Format("{0:0.00}", pointOfOneCorrectAnswer); 
                } 
                else if (!option.IsCorrect)
                {
                    option.Percentages = percentOfOneInCorrectAnswer;
                    option.Points = String.Format("{0:0.00}", pointOfOneInCorrectAnswer);
                }
            }
        }
    }
}
