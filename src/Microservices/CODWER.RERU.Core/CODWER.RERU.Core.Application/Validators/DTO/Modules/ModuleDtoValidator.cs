using CODWER.RERU.Core.DataTransferObjects.Modules;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Validators.DTO.Modules
{
    public class ModuleDtoValidator : AbstractValidator<ModuleDto>
    {
        public ModuleDtoValidator()
        {
            When(x => !string.IsNullOrEmpty(x.Color), () =>
            {
                RuleFor(x => x.Color).ColorRule();
            });
        }
    }
}