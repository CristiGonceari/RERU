using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.FieldTypes.Validators
{
    public class CharacterValueValidator : AbstractValidator<string>
    {
        public CharacterValueValidator()
        {
            RuleFor(x => x).Custom(ValidateCharacter);
        }

        private void ValidateCharacter(string value, CustomContext context)
        {
            if (!char.TryParse(value, out _))
            {
                context.AddFail(ValidationCodes.INVALID_CHARACTER_VALUE, "Invalid character value");
            }
        }
    }
}
