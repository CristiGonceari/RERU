using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.DepartmentRoleContent;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.AddDepartmentRoleContent
{
    public class AddDepartmentRoleContentCommandValidator : AbstractValidator<AddDepartmentRoleContentCommand>
    {
        public AddDepartmentRoleContentCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data)
                .SetValidator(new ExistentDepartmentRoleContentRecordValidator(appDbContext, ValidationMessages.InvalidInput));

            RuleFor(x => x.Data)
                    .Must(x => x.OrganizationRoleCount > 0)
                    .WithMessage(ValidationMessages.InvalidInput)
                    .WithErrorCode(ValidationCodes.INVALID_INPUT);
        }
    }
}
