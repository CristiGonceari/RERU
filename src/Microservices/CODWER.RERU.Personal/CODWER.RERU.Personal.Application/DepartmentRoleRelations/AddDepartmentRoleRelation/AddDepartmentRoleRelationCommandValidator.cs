using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.DepartmentRoleRelations;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddDepartmentRoleRelation
{
    public class AddDepartmentRoleRelationCommandValidator : AbstractValidator<AddDepartmentRoleRelationCommand>
    {
        public AddDepartmentRoleRelationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.RelationType).IsInEnum();

            RuleFor(x => x.Data)
                .SetValidator(new ExistentDepartmentRoleRelationRecordValidator(appDbContext, ValidationMessages.InvalidInput));

            RuleFor(x => x.Data.OrganizationalChartId)
                .SetValidator(new ItemMustExistValidator<OrganizationalChart>(appDbContext, ValidationCodes.ORGANIZATIONAL_CHART_NOT_FOUND,ValidationMessages.InvalidReference));
        }
    }
}
