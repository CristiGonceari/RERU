using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using System;

namespace CODWER.RERU.Core.Application.Validators.EnumValidators
{
    public class ExistInEnumValidator<T> : AbstractValidator<int>
    {
        public ExistInEnumValidator()
        {
            RuleFor(x => x).Custom(ExistInEnum);
        }

        private void ExistInEnum(int number, CustomContext context)
        {
            if (!Enum.IsDefined(typeof(T), number))
            {
                context.AddFail(ValidationCodes.INVALID_INPUT, ValidationMessages.InvalidInput);
            }
        }
    }
}
