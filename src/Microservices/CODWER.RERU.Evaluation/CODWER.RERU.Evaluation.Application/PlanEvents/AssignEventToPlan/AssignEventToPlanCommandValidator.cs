using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.AssignEventToPlan
{
    public class AssignEventToPlanCommandValidator : AbstractValidator<AssignEventToPlanCommand>
    {
        public AssignEventToPlanCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(r => r.Data.PlanId)
                    .Must(x => appDbContext.Plans.Any(l => l.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_PLAN);

                RuleFor(r => r.Data.EventId)
                    .Must(x => appDbContext.Events.Any(l => l.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_EVENT);
            });
        }
    }

}
