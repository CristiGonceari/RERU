using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.UnassignResponsiblePersonFromLocation
{
    public class UnassignResponsiblePersonFromLocationCommandValidator : AbstractValidator<UnassignResponsiblePersonFromLocationCommand>
    {
        public UnassignResponsiblePersonFromLocationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data)
                    .Must(x => appDbContext.LocationResponsiblePersons.Any(l => l.LocationId == x.LocationId && l.UserProfileId == x.UserProfileId))
                    .WithErrorCode(ValidationCodes.INVALID_RECORD);

                RuleFor(x => x.Data.LocationId)
                    .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_LOCATION,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.UserProfileId)
                    .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_USER,
                        ValidationMessages.InvalidReference));
            });
        }
    }
}
