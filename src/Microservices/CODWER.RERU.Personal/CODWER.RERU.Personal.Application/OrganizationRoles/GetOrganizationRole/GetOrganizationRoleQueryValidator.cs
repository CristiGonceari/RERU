using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRole
{
    public class GetOrganizationRoleQueryValidator : AbstractValidator<GetOrganizationRoleQuery>
    {
        public GetOrganizationRoleQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<OrganizationRole>(appDbContext,ValidationCodes.ORGANIZATION_ROLE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
