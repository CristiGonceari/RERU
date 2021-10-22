using CODWER.RERU.Core.Application.ModuleRoles.AddModuleRole;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using FluentValidation;

namespace CODWER.RERU.Core.Application.ModuleRoles.EditModuleRole
{
    public class EditModuleRoleCommanValidator : AbstractValidator<CreateModuleRoleCommand>
    {
        public EditModuleRoleCommanValidator(IValidator<AddEditModuleRoleDto> addEditModuleRoleDto)
        {
            RuleFor(x => x.ModuleRole)
                .SetValidator(addEditModuleRoleDto);
        }
    }
}
