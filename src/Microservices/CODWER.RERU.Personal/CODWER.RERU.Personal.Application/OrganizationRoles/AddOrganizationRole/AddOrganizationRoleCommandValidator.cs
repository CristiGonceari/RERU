using FluentValidation;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.AddOrganizationRole
{
    public class AddOrganizationRoleCommandValidator : AbstractValidator<AddOrganizationRoleCommand>
    {
        public AddOrganizationRoleCommandValidator()
        {
            RuleFor(x => x.Data).SetValidator(new OrganizationRoleValidator());
        }
    }
}
