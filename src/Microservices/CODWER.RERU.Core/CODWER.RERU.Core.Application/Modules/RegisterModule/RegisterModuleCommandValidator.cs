using CODWER.RERU.Core.DataTransferObjects.Modules;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Modules.RegisterModule
{
    public class RegisterModuleCommandValidator : AbstractValidator<RegisterModuleCommand>
    {
        public RegisterModuleCommandValidator(IValidator<AddEditModuleDto> moduleDto)
        {
            RuleFor(x => x.Module)
                .SetValidator(moduleDto);
        }
    }
}
