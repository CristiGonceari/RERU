using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Departments.GetDepartment
{
    public class GetDepartmentQueryValidator : AbstractValidator<GetDepartmentQuery>
    {
        public GetDepartmentQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.DEPARTMENT_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
