using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.FieldTypes.Validators
{
    public class BooleanValueValidator : AbstractValidator<string>
    {
        public BooleanValueValidator()
        {
            RuleFor(x => x).Custom(ValidateInteger);
        }

        private void ValidateInteger(string value, CustomContext context)
        {
            if (!bool.TryParse(value, out _))
            {
                context.AddFail(ValidationCodes.INVALID_BOOLEAN_VALUE, "Invalid boolean value");
            }
        }
    }
}
