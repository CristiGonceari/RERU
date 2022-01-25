using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.RemoveDepartmentRoleRelation
{
    public class RemoveDepartmentRoleRelationValidator : AbstractValidator<RemoveDepartmentRoleRelationCommand>
    {
        public RemoveDepartmentRoleRelationValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id).SetValidator(new ItemMustExistValidator<DepartmentRoleRelation>(appDbContext,ValidationCodes.ORGANIZATION_ROLE_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
