using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Validators.DTO.Users
{
    public class CreateModuleRoleDtoValidator : AbstractValidator<AddEditModuleRoleDto>
    {
        public CreateModuleRoleDtoValidator()
        {
            RuleFor(x => x.Name).NameRule();
        }
    }
}