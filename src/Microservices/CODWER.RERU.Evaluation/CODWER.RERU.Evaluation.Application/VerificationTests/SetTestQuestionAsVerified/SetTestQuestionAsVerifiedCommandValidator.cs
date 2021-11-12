using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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
