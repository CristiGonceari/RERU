using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.UnassignPlanResponsiblePerson
{
    public class UnassignPlanResponsiblePersonCommandValidator : AbstractValidator<UnassignPlanResponsiblePersonCommand>
    {
        public UnassignPlanResponsiblePersonCommandValidator(AppDbContext appDbContext)
        {
                RuleFor(x => x)
                    .Must(x => appDbContext.PlanResponsiblePersons.Any(l => l.PlanId == x.PlanId && l.UserProfileId == x.UserProfileId))
                    .WithErrorCode(ValidationCodes.INVALID_ID);

            RuleFor(x => x.PlanId)
                .SetValidator(x => new ItemMustExistValidator<Plan>(appDbContext, ValidationCodes.INVALID_PLAN,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.UserProfileId)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                    ValidationMessages.InvalidReference));
        }
    }
}
