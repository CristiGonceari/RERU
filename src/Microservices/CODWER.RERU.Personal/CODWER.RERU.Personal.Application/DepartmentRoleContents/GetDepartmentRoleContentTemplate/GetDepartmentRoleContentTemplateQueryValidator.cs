using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentTemplate
{
    public class GetDepartmentRoleContentTemplateQueryValidator : AbstractValidator<GetDepartmentRoleContentTemplateQuery>
    {
        public GetDepartmentRoleContentTemplateQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.DepartmentId)
                .SetValidator(new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.DEPARTMENT_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
