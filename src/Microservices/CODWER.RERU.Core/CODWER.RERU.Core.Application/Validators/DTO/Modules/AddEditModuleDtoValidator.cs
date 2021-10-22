using CODWER.RERU.Core.DataTransferObjects.Modules;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Validators.DTO.Modules
{
    public class AddEditModuleDtoValidator : AbstractValidator<AddEditModuleDto>
    {
        public AddEditModuleDtoValidator()
        {
            When(x => !string.IsNullOrEmpty(x.Color), () =>
            {
                RuleFor(x => x.Color).ColorRule();
            });
        }
    }
}