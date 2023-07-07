using FluentValidation;

namespace CODWER.RERU.Core.Application.Validators.DTO.Users 
{
    public static class UserRuleBuilders 
    {
        public static IRuleBuilderOptions<T, string> NameRule<T> (this IRuleBuilder<T, string> ruleBuilder) {
            return ruleBuilder
                .NotNull()
                .WithErrorCode(MessageCodes.INVALID_INPUT)
                .WithMessage(MessageCodes.NAME_SHOULD_NOT_BE_NULL)
                .NotEmpty()
                .WithErrorCode(MessageCodes.INVALID_INPUT)
                .WithMessage(MessageCodes.NAME_SHOULD_NOT_BE_EMPTY)
                .MinimumLength(1)
                .WithErrorCode(MessageCodes.INVALID_INPUT)
                .WithMessage(MessageCodes.MINIMUM_LENGTH_REQUIRED)
                .Matches("^(?! )[a-zA-ZăâîșşțţĂÂÎȘŞȚŢ][a-zA-ZăâîșşțţĂÂÎȘŞȚŢ0-9-_.]{0,20}$|^[a-zA-ZăâîșşțţĂÂÎȘŞȚŢ][a-zA-ZăâîșşțţĂÂÎȘŞȚŢ0-9-_. ]*[a-zA-ZăâîșşțţĂÂÎȘŞȚŢ][a-zA-ZăâîșşțţĂÂÎȘŞȚŢ0-9-_.]{0,20}$")
                .WithErrorCode(MessageCodes.INVALID_INPUT)
                .WithMessage(MessageCodes.NAME_SHOULD_NOT_START_WITH_NUMBER);
        }
    }
}