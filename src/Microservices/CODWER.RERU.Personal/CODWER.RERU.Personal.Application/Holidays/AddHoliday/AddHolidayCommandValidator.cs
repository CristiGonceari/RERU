using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Holidays.AddHoliday
{
    public class AddHolidayCommandValidator : AbstractValidator<AddHolidayCommand>
    {
        public AddHolidayCommandValidator()
        {
            RuleFor(x => x.Data.Name).NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithErrorCode(ValidationMessages.InvalidInput);

            When(x=>x.Data.To != null, () =>
            {
                RuleFor(x => x.Data).Must(x => x.From < x.To)
                    .WithErrorCode(ValidationCodes.INVALID_INPUT)
                    .WithMessage(ValidationMessages.InvalidInput);
            });
        }
    }
}
