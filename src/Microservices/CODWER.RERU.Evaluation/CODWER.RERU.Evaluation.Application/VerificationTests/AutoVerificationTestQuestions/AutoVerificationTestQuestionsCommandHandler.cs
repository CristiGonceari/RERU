using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.AutoVerificationTestQuestions
{
    public class AutoVerificationTestQuestionsCommandHandler : IRequestHandler<AutoVerificationTestQuestionsCommand, Response>
    {
        private readonly AppDbContext _appDbContext;

        public AutoVerificationTestQuestionsCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Response> Handle(AutoVerificationTestQuestionsCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestQuestions)
                    .ThenInclude(x => x.QuestionUnit)
                .Include(x => x.TestQuestions)
                    .ThenInclude(x => x.TestAnswers)
                .Include(x => x.TestQuestions)
                    .ThenInclude(x => x.QuestionUnit)
                    .ThenInclude(x => x.Options)
                .FirstAsync(x => x.Id == request.TestId);

            var testTemplate = _appDbContext.TestTemplates
                .Include(x => x.Settings)
                .FirstOrDefault(x => x.Id == test.TestTemplateId);

            foreach (var testQuestion in test.TestQuestions)
            {
                if (testQuestion.AnswerStatus == AnswerStatusEnum.Answered)
                {
                    if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer || testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.MultipleAnswers)
                    {
                        CalculateScoreByFormula(testQuestion, testTemplate);
                    }
                    else
                    {
                        testQuestion.Verified = VerificationStatusEnum.NotVerified;
                    }
                }
                else
                {
                    if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer || testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.MultipleAnswers)
                    {
                        testQuestion.IsCorrect = false;
                        testQuestion.Points = 0;
                        testQuestion.Verified = VerificationStatusEnum.VerifiedBySystem;
                    }
                }
            }

            await _appDbContext.SaveChangesAsync();

            return new Response();
        }

        private void CalculateScoreByFormula(TestQuestion testQuestion, TestTemplate testTemplate)
        {
            var answers = _appDbContext.TestAnswers.Where(x => x.TestQuestionId == testQuestion.Id).ToList();
            var correctOptions = testQuestion.QuestionUnit.Options.Where(x => x.IsCorrect).Count();
            var suma = 0;

            var percentOfOneCorrectAnswer = (int)Math.Round((double)(100 / correctOptions));
            var percentOfOneInCorrectAnswer = 0;

            if(testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer)
            {
                if (testTemplate.Settings.FormulaForOneAnswer == ScoreFormulaEnum.MinusCorrectOption)
                {
                    percentOfOneInCorrectAnswer = percentOfOneCorrectAnswer * (-1);
                }
                else if (testTemplate.Settings.FormulaForOneAnswer == ScoreFormulaEnum.OneDivideCountPercent)
                {
                    percentOfOneInCorrectAnswer = (int)Math.Round(((double)1 / testQuestion.QuestionUnit.Options.Count()) * 100) * -1;
                }
            } 
            else if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.MultipleAnswers)
            {
                if (testTemplate.Settings.FormulaForMultipleAnswers == ScoreFormulaEnum.MinusCorrectOption)
                {
                    percentOfOneInCorrectAnswer = percentOfOneCorrectAnswer * (-1);
                }
                else if (testTemplate.Settings.FormulaForMultipleAnswers == ScoreFormulaEnum.OneDivideCountPercent)
                {
                    percentOfOneInCorrectAnswer = (int)Math.Round(((double)1 / testQuestion.QuestionUnit.Options.Count()) * 100) * -1;
                }
            }

            foreach (var answer in answers)
            {
                var option = testQuestion.QuestionUnit.Options.FirstOrDefault(x => x.Id == answer.OptionId);

                if (option.IsCorrect)
                {
                    suma += percentOfOneCorrectAnswer;
                }
                else if (!option.IsCorrect)
                {
                    suma += percentOfOneInCorrectAnswer;
                }
            }

            if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer && testTemplate.Settings.NegativeScoreForOneAnswer == false)
            {
                if (suma < 0)
                {
                    suma = 0;
                }
            }

            if (testQuestion.QuestionUnit.QuestionType == QuestionTypeEnum.MultipleAnswers && testTemplate.Settings.NegativeScoreForMultipleAnswers == false)
            {
                if (suma < 0)
                {
                    suma = 0;
                }
            }

            testQuestion.Points = (int)Math.Round((double)testQuestion.QuestionUnit.QuestionPoints * suma / 100);

            if (suma > 0)
            {
                testQuestion.IsCorrect = true;
            }
            else
            {
                testQuestion.IsCorrect = false;
            }

            testQuestion.Verified = VerificationStatusEnum.VerifiedBySystem;
        }
    }
}
