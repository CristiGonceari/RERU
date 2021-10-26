using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using FluentValidation;

namespace CODWER.RERU.Core.Application.ModuleRoles.AddModuleRole
{
    public class CreateModuleRoleCommandValidator : AbstractValidator<CreateModuleRoleCommand>
    {
        public CreateModuleRoleCommandValidator(IValidator<AddEditModuleRoleDto> addEditModuleRoleDto)
        {
            RuleFor(x => x.ModuleRole)
                .SetValidator(addEditModuleRoleDto);
        }
    }
}
