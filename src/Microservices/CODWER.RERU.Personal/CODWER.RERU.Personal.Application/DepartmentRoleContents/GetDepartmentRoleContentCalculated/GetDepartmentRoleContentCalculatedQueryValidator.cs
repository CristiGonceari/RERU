using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentCalculated
{
    public class GetDepartmentRoleContentCalculatedQueryValidator : AbstractValidator<GetDepartmentRoleContentCalculatedQuery>
    {
        public GetDepartmentRoleContentCalculatedQueryValidator(AppDbContext appDbContext)
        {
            When(x => x.Type == 1, () =>
            {
                RuleFor(x => x.Id)
              .SetValidator(new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.DEPARTMENT_NOT_FOUND, ValidationMessages.InvalidReference));
            });

            When(x => x.Type == 2, () =>
            {
                RuleFor(x => x.Id)
              .SetValidator(new ItemMustExistValidator<Role>(appDbContext, ValidationCodes.ORGANIZATION_ROLE_NOT_FOUND, ValidationMessages.InvalidReference));
            });

        }
    }
}
