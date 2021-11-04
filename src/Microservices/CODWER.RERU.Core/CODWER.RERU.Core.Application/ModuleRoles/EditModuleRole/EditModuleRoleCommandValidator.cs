using CODWER.RERU.Core.Application.ModuleRoles.AddModuleRole;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using FluentValidation;

namespace CODWER.RERU.Core.Application.ModuleRoles.EditModuleRole
{
    public class EditModuleRoleCommandValidator : AbstractValidator<CreateModuleRoleCommand>
    {
        public EditModuleRoleCommandValidator(IValidator<AddEditModuleRoleDto> addEditModuleRoleDto)
        {
            RuleFor(x => x.ModuleRole)
                .SetValidator(addEditModuleRoleDto);
        }
    }
}
