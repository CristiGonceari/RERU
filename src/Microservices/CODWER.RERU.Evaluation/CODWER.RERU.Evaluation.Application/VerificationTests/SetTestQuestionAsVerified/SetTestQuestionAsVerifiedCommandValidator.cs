using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.SetTestQuestionAsVerified
{
    public class SetTestQuestionAsVerifiedCommandValidator : AbstractValidator<SetTestQuestionAsVerifiedCommand>
    {
        public SetTestQuestionAsVerifiedCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data)
                .Must(x => appDbContext.TestQuestions
                    .Any(t => t.Id == appDbContext.Tests
                        .FirstOrDefault(ts => ts.Id == x.TestId)
                        .TestQuestions
                        .FirstOrDefault(q => q.Index == x.QuestionIndex).Id))
                .WithErrorCode(ValidationCodes.INVALID_TEST_QUESTION);

            RuleFor(x => x.Data.IsCorrect)
                .NotNull()
                .WithErrorCode(ValidationCodes.INVALID_STATUS_FOR_VERIFY);

            RuleFor(x => x.Data)
                .Must(x => appDbContext.TestQuestions
                    .Include(x => x.QuestionUnit)
                    .First(s => s.Index == x.QuestionIndex && s.TestId == x.TestId).QuestionUnit.QuestionType != QuestionTypeEnum.OneAnswer)
                .WithErrorCode(ValidationCodes.EMPTY_QUESTION_TYPE);

            RuleFor(x => x.Data)
                .Must(x => appDbContext.QuestionUnits.Any(q => q.Id == x.QuestionUnitId && q.QuestionPoints >= x.EvaluatorPoints))
                .WithErrorCode(ValidationCodes.QUESTION_POINTS_LIMIT_WRONG);
        }
    }
}
