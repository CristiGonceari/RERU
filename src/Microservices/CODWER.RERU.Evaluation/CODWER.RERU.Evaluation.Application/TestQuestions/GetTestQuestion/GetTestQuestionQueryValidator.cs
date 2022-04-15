using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GetTestQuestion
{
    public class GetTestQuestionQueryValidator : AbstractValidator<GetTestQuestionQuery>
    {
        private readonly AppDbContext _appDbContext;
        public GetTestQuestionQueryValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x)
               .Must(x => appDbContext.TestQuestions
               .Any(t => t.Id == appDbContext.Tests.FirstOrDefault(ts => ts.Id == x.TestId).TestQuestions.FirstOrDefault(q => q.Index == x.QuestionIndex).Id))
               .WithErrorCode(ValidationCodes.INVALID_TEST_QUESTION);

            When(x => appDbContext.Tests.Include(x => x.TestTemplate).ThenInclude(x => x.Settings).FirstOrDefault(t => t.Id == x.TestId).TestTemplate.Settings.MaxErrors.HasValue, () =>
            {
                RuleFor(x => x)
                .Must(x => ValidateMaxErrors(x.TestId))
                .WithErrorCode(ValidationCodes.REACHED_ERRORS_LIMIT);
            });

            When(x => !appDbContext.Tests.Include(x => x.TestTemplate).ThenInclude(x => x.Settings).FirstOrDefault(t => t.Id == x.TestId).TestTemplate.Settings.PossibleChangeAnswer, () =>
            {
                RuleFor(x => x)
                .Must(x => appDbContext.Tests.Include(x => x.TestQuestions).First(t => t.Id == x.TestId).TestQuestions.First(q => q.Index == x.QuestionIndex).AnswerStatus != AnswerStatusEnum.Answered)
                .WithErrorCode(ValidationCodes.CANT_CHANGE_ANSWER);
            });

            When(x => !appDbContext.Tests.Include(x => x.TestTemplate).ThenInclude(x => x.Settings).First(t => t.Id == x.TestId).TestTemplate.Settings.PossibleGetToSkipped, () =>
            {
                RuleFor(x => x)
                .Must(x => appDbContext.Tests.Include(x => x.TestQuestions).First(t => t.Id == x.TestId).TestQuestions.First(q => q.Index == x.QuestionIndex).AnswerStatus != AnswerStatusEnum.Skipped)
                .WithErrorCode(ValidationCodes.CANT_RETURN_TO_QUESTION);
            });

            When(x => appDbContext.Tests.Include(x => x.TestTemplate).First(t => t.Id == x.TestId).TestTemplate.Mode == TestTemplateModeEnum.Test, () =>
            {
                RuleFor(x => x)
                    .Must(x => appDbContext.Tests.FirstOrDefault(t => t.Id == x.TestId).TestStatus == TestStatusEnum.InProgress
                                                                && appDbContext.Tests.FirstOrDefault(t => t.Id == x.TestId).EndTime > DateTime.Now)
                    .WithErrorCode(ValidationCodes.TEST_IS_FINISHED);
            });
        }

        private bool ValidateMaxErrors(int testId)
        {
            var test = _appDbContext.Tests
                .Include(x => x.TestQuestions)
                    .ThenInclude(x => x.QuestionUnit)
                        .ThenInclude(x => x.Options)
                .Include(x => x.TestQuestions)
                    .ThenInclude(x => x.TestAnswers)
                .First(x => x.Id == testId);

            var questions = test.TestQuestions.Where(x => x.AnswerStatus == AnswerStatusEnum.Answered && (x.QuestionUnit.QuestionType == QuestionTypeEnum.MultipleAnswers || x.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer));
            var countedErrors = 0;

            foreach (var testQuestion in questions)
            {
                var correctAnswer = true;

                foreach (var option in testQuestion.QuestionUnit.Options)
                {
                    var answer = testQuestion.TestAnswers.FirstOrDefault(x => x.OptionId.Value == option.Id);

                    if (answer == null)
                    {
                        if (option.IsCorrect == false)
                        {
                            continue;
                        }
                        else
                        {
                            correctAnswer = false;
                            break;
                        }
                    }
                    else
                    {
                        if (option.IsCorrect)
                        {
                            continue;
                        }
                        else
                        {
                            correctAnswer = false;
                            break;
                        }
                    }
                }

                if (!correctAnswer)
                {
                    countedErrors++;
                }
            }

            if (test.MaxErrors < countedErrors)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
