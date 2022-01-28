using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;
using System.Linq;
using System.Text.RegularExpressions;

namespace CODWER.RERU.Core.Application.Validators.IDNP
{
    public class IdnpValidator : AbstractValidator<string>
    {
        public IdnpValidator()
        {
            RuleFor(x => x).Custom(Validate);
        }

        private void Validate(string value, CustomContext context)
        {
            if (value.Length != 13)
            {
                context.AddFail(ValidationCodes.VALIDATION_IDNP_SHOULD_BE_13_DIGITS, MessageCodes.INVALID_INPUT);
                return;
            }

            if (!Regex.IsMatch(value, "^[0-9]*$"))
            {
                context.AddFail(ValidationCodes.VALIDATION_IDNP_SHOULD_BE_ONLY_DIGITS, MessageCodes.INVALID_INPUT);
                return;
            }

            if (value.ElementAt(0) != '0' && value.ElementAt(0) != '2')
            {
                context.AddFail(ValidationCodes.VALIDATION_IDNP_SHOULD_BE_ONLY_DIGITS, MessageCodes.INVALID_INPUT);
                return;
            }

            if (value == "0000000000000")
            {
                context.AddFail(ValidationCodes.VALIDATION_IDNP_SHOULD_NOT_BE_ZEROES, MessageCodes.INVALID_INPUT);
                return;
            }

            int crc = 0;
            int num;

            for (int i = 0; i < value.Length - 1; i++)
            {
                num = int.Parse(value[i].ToString());
                crc += num * ((i % 3 == 0) ? 7 : (i % 3 == 1) ? 3 : 1);
            }

            num = int.Parse(value[12].ToString());

            if ((crc % 10) != num)
            {
                context.AddFail(ValidationCodes.VALIDATION_IDNP_INVALID_CONTROL_DIGIT, MessageCodes.INVALID_INPUT);
            }
        }
    }
}
