using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.RemoveDepartmentRoleRelation
{
    public class RemoveDepartmentRoleRelationValidator : AbstractValidator<RemoveDepartmentRoleRelationCommand>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveDepartmentRoleRelationValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Id).SetValidator(new ItemMustExistValidator<DepartmentRoleRelation>(appDbContext,ValidationCodes.ORGANIZATION_ROLE_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
