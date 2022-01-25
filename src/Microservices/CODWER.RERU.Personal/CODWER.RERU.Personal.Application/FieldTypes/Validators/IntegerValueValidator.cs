using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.FieldTypes.Validators
{
    public class IntegerValueValidator : AbstractValidator<string>
    {
        public IntegerValueValidator()
        {
            RuleFor(x => x).Custom(ValidateInteger);
        }

        private void ValidateInteger(string value, CustomContext context)
        {
            if (!int.TryParse(value, out _))
            {
                context.AddFail(ValidationCodes.INVALID_INTEGER_VALUE, "Invalid integer value");
            }
        }
    }
}
