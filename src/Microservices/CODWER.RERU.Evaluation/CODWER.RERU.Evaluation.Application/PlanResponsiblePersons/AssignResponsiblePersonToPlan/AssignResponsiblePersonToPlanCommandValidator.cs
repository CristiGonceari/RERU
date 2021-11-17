using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.AssignResponsiblePersonToPlan
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
                RuleFor(x => x.Data.PlanId)
                    .SetValidator(x => new ItemMustExistValidator<Plan>(appDbContext, ValidationCodes.INVALID_PLAN,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.UserProfileId)
                    .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.PlanResponsiblePersons.Any(l => l.PlanId == x.PlanId && l.UserProfileId == x.UserProfileId))
                    .WithErrorCode(ValidationCodes.USER_ALREADY_ASSIGNED);
            });
        }
    }

}
