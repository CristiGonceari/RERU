using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.FieldTypes.Validators
{
    public class DoubleValueValidator : AbstractValidator<string>
    {
        public DoubleValueValidator()
        {
            RuleFor(x => x).Custom(ValidateDouble);
        }

        private void ValidateDouble(string value, CustomContext context)
        {
            if (!double.TryParse(value, out _))
            {
                context.AddFail(ValidationCodes.INVALID_DOUBLE_VALUE, "Invalid double value");
            }
        }
    }
}
