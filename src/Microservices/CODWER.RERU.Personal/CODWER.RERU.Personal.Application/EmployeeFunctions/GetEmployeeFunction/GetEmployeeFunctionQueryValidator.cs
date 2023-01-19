using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions.GetEmployeeFunction
{
    public class GetEmployeeFunctionQueryValidator : AbstractValidator<GetEmployeeFunctionQuery>
    {
        public GetEmployeeFunctionQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<EmployeeFunction>(appDbContext, ValidationCodes.EMPLOYEE_FUNCTION_NOT_NOUD, ValidationMessages.NotFound));
        }
    }
}
