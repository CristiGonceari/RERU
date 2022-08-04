using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.RemoveDepartmentRoleContent
{
    public class RemoveDepartmentRoleContentCommandValidator : AbstractValidator<RemoveDepartmentRoleContentCommand>
    {
        public RemoveDepartmentRoleContentCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<DepartmentRoleContent>(appDbContext,ValidationCodes.DEPARTMENT_ROLE_CONTENT_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
