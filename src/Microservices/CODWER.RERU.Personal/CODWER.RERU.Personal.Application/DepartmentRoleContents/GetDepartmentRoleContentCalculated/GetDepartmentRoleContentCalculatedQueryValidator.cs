using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentCalculated
{
    public class GetDepartmentRoleContentCalculatedQueryValidator : AbstractValidator<GetDepartmentRoleContentCalculatedQuery>
    {
        public GetDepartmentRoleContentCalculatedQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.DepartmentId)
                .SetValidator(new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.DEPARTMENT_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
