using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.Configurations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

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
