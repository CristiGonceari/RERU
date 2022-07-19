using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.RemoveOrganizationRole
{
    public class RemoveRoleCommandValidator : AbstractValidator<RemoveRoleCommand>
    {
        public RemoveRoleCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Role>(appDbContext, ValidationCodes.ORGANIZATION_ROLE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
