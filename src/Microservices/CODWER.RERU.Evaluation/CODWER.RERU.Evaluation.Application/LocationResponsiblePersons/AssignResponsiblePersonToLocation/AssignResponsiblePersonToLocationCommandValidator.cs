using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.AssignResponsiblePersonToLocation
{
    public class AssignResponsiblePersonToLocationCommandValidator : AbstractValidator<AssignResponsiblePersonToLocationCommand>
    {
        public AssignResponsiblePersonToLocationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(r => r.Data.LocationId)
                 .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_LOCATION,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data.UserProfileId)
                .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_USER_PROFILE,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.LocationResponsiblePersons.Any(l => l.LocationId == x.LocationId && l.UserProfileId == x.UserProfileId))
                    .WithErrorCode(ValidationCodes.EXISTENT_RESPONSIBLE_PERSON_IN_LOCATION);
            });
        }
    }
}
