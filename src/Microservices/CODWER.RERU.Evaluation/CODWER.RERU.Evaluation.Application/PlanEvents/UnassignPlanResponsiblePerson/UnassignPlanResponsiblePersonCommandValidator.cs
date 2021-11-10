using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanResponsiblePerson
{
    public class UnassignPlanResponsiblePersonCommandValidator : AbstractValidator<UnassignPlanResponsiblePersonCommand>
    {
        public UnassignPlanResponsiblePersonCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data)
                    .Must(x => appDbContext.PlanResponsiblePersons.Any(l => l.PlanId == x.PlanId && l.UserProfileId == x.UserProfileId))
                    .WithErrorCode(ValidationCodes.INVALID_ID);

                RuleFor(x => x.Data.PlanId)
                    .GreaterThan(0)
                    .WithErrorCode(ValidationCodes.INVALID_PLAN);

                RuleFor(x => x.Data.UserProfileId)
                    .GreaterThan(0)
                    .WithErrorCode(ValidationCodes.INVALID_ID);
            });
        }
    }

}
