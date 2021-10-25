using FluentValidation;

namespace CODWER.RERU.Core.Application.Validators.DTO.Modules
{
    public static class ModuleRuleBuilders
    {
        public static IRuleBuilderOptions<T, string> ColorRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder              
                .Matches("^#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$")
                .WithErrorCode(MessageCodes.INVALID_INPUT);
        }
    }
}