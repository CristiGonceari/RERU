using System;
using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.FieldTypes.Validators
{
    class DateTimeValueValidator : AbstractValidator<string>
    {
        public DateTimeValueValidator()
        {
            RuleFor(x => x).Custom(ValidateDateTime);
        }

        private void ValidateDateTime(string value, CustomContext context)
        {
            if (!DateTime.TryParse(value, out _))
            {
                context.AddFail(ValidationCodes.INVALID_DATETIME_VALUE, "Invalid dateTime value");
            }
        }
    }
}
