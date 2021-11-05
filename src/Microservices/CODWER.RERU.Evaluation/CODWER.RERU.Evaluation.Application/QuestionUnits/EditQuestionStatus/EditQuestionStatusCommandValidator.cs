using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionStatus
{
    public class EditQuestionStatusCommandValidator : AbstractValidator<EditQuestionStatusCommand>
    {
        public EditQuestionStatusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.QuestionId)
                    .GreaterThan(0)
                    .Must(x => appDbContext.QuestionUnits.Any(l => l.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION);

                RuleFor(r => r.Data.Status)
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.INVALID_STATUS);

                When(x => 
                    appDbContext.Tests.Include(x => x.TestQuestions).ThenInclude(x => x.QuestionUnit)
                              .Where(t => t.TestQuestions.Any(tq => tq.QuestionUnitId == x.Data.QuestionId)).Any(s => s.TestStatus > TestStatusEnum.AlowedToStart) &&
                    appDbContext.Tests.Include(x => x.TestQuestions).ThenInclude(x => x.QuestionUnit)
                              .Where(t => t.TestQuestions.Any(tq => tq.QuestionUnitId == x.Data.QuestionId)).Any(s => s.TestStatus <= TestStatusEnum.Verified), () =>
                {
                    RuleFor(x => x.Data.Status)
                        .Must(x => x == QuestionUnitStatusEnum.Inactive)
                        .WithErrorCode(ValidationCodes.QUESTION_IS_IN_ACTIVE_TEST_TYPE);
                });
            });
        }
    }
}
