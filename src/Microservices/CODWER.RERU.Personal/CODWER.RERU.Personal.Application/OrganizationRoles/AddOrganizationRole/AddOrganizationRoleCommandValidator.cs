using FluentValidation;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.AddOrganizationRole
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(x => x.Data).SetValidator(new RoleValidator());
        }
    }
}
