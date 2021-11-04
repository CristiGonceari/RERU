using CODWER.RERU.Core.DataTransferObjects.Modules;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Modules.EditModule
{
    public class EditModuleCommandValidator : AbstractValidator<EditModuleCommand>
    {
        public EditModuleCommandValidator(IValidator<AddEditModuleDto> moduleDto)
        {
            RuleFor(x => x.Module)
                .SetValidator(moduleDto);
        }
    }
}
