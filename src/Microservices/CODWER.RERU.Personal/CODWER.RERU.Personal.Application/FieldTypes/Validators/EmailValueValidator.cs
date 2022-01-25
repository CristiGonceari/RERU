using CODWER.RERU.Personal.Application.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.FieldTypes.Validators
{
    public class EmailValueValidator : AbstractValidator<string>
    {
        public EmailValueValidator()
        {
            RuleFor(x => x).SetValidator(new TextValueValidator());

            RuleFor(x => x).EmailAddress()
                .WithMessage("Invalid email value")
                .WithErrorCode(ValidationCodes.INVALID_EMAIL_VALUE);
        }
    }
}
