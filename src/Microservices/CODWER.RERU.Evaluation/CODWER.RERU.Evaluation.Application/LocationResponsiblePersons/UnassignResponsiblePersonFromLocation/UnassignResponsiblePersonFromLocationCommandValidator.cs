using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using System.Linq;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.UnassignResponsiblePersonFromLocation
{
    public class UnassignResponsiblePersonFromLocationCommandValidator : AbstractValidator<UnassignResponsiblePersonFromLocationCommand>
    {
        public UnassignResponsiblePersonFromLocationCommandValidator(AppDbContext appDbContext)
        {
                RuleFor(x => x)
                    .Must(x => appDbContext.LocationResponsiblePersons.Any(l => l.LocationId == x.LocationId && l.UserProfileId == x.UserProfileId))
                    .WithErrorCode(ValidationCodes.INVALID_RECORD);

                RuleFor(x => x.LocationId)
                    .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_LOCATION,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.UserProfileId)
                    .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                        ValidationMessages.InvalidReference));
        }
    }
}
