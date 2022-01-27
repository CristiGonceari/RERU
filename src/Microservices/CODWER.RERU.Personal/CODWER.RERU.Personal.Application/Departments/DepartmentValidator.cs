using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Departments
{
    public class DepartmentValidator : AbstractValidator<AddEditDepartmentDto>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);
        }
    }
}
