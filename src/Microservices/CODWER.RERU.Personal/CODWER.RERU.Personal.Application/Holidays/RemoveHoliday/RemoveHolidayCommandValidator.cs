using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities.Configurations;

namespace CODWER.RERU.Personal.Application.Holidays.RemoveHoliday
{
    public class RemoveHolidayCommandValidator : AbstractValidator<RemoveHolidayCommand>
    {
        public RemoveHolidayCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Holiday>(appDbContext, ValidationCodes.HOLIDAY_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
