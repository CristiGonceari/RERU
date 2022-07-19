using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validators.UserProfiles;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Vacations.UpdateVacation
{
    public class UpdateVacationCommandValidator : AbstractValidator<UpdateVacationCommand>
    {
        public UpdateVacationCommandValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            //RuleFor(x => x.Data.Id)
            //    .SetValidator(new ItemMustExistValidator<Vacation>(appDbContext, ValidationCodes.VACATION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new VacationValidator(appDbContext));

            RuleFor(x => x.Data).SetValidator(new ExistentCurrentUserProfileAndContractor(userProfileService, ValidationMessages.InvalidInput));
        }
    }
}
