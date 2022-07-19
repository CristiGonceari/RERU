using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities.Configurations;

namespace CODWER.RERU.Personal.Application.Holidays.UpdateHoliday
{
    public class UpdateHolidayCommandValidator : AbstractValidator<UpdateHolidayCommand>
    {
        public UpdateHolidayCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Holiday>(appDbContext, ValidationCodes.HOLIDAY_NOT_FOUND, ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.Name).NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithErrorCode(ValidationMessages.InvalidInput);

            When(x => x.Data.To != null, () =>
            {
                RuleFor(x => x.Data).Must(x => x.From < x.To)
                    .WithErrorCode(ValidationCodes.INVALID_INPUT)
                    .WithMessage(ValidationMessages.InvalidInput);
            });
        }
    }
}
