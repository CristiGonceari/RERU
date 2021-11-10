using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.AssignResponsiblePersonToPlan
{
    public class AssignResponsiblePersonToPlanCommandValidator : AbstractValidator<AssignResponsiblePersonToPlanCommand>
    {
        public AssignResponsiblePersonToPlanCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(r => r.Data.PlanId)
                    .Must(x => appDbContext.Plans.Any(l => l.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_PLAN);

                RuleFor(r => r.Data.UserProfileId)
                    .Must(x => appDbContext.UserProfiles.Any(l => l.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_ID);

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.PlanResponsiblePersons.Any(l => l.PlanId == x.PlanId && l.UserProfileId == x.UserProfileId))
                    .WithErrorCode(ValidationCodes.USER_ALREADY_ASSIGNED);
            });
        }
    }

}
