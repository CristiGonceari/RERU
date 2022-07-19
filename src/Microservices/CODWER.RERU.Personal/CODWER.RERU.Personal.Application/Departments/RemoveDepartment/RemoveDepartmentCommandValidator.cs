using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Departments.RemoveDepartment
{
    public class RemoveDepartmentCommandValidator : AbstractValidator<RemoveDepartmentCommand>
    {
        public RemoveDepartmentCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.DEPARTMENT_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
