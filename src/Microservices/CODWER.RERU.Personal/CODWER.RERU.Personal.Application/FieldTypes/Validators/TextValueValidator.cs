using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.FieldTypes.Validators
{
    public class TextValueValidator : AbstractValidator<string>
    {
        public TextValueValidator()
        {
            RuleFor(x => x).Custom(ValidateText);
        }

        private void ValidateText(string value, CustomContext context)
        {
            if (value.Length > 550)
            {
                context.AddFail(ValidationCodes.INVALID_TEXT_VALUE, "Invalid text value");
            }
        }
    }
}
