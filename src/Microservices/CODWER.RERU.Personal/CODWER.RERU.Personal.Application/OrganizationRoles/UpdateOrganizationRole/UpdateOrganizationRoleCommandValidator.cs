using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.UpdateOrganizationRole
{
    public class UpdateOrganizationRoleCommandValidator : AbstractValidator<UpdateOrganizationRoleCommand>
    {
        public UpdateOrganizationRoleCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<OrganizationRole>(appDbContext, ValidationCodes.ORGANIZATION_ROLE_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new OrganizationRoleValidator());
        }
    }
}
