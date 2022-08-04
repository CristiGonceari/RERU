using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Departments.RemoveDepartment
{
    public class RemoveDepartmentCommandValidator : AbstractValidator<RemoveDepartmentCommand>
    {
        public RemoveDepartmentCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));
        }
    }
}
