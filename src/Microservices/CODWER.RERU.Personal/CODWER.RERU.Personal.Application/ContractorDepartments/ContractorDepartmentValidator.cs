using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.ContractorDepartments
{
    public class ContractorDepartmentValidator : AbstractValidator<AddEditContractorDepartmentDto>
    {
        public ContractorDepartmentValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.InvalidReference));

            RuleFor(x => x.DepartmentId)
                .SetValidator(new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.DEPARTMENT_NOT_FOUND, ValidationMessages.InvalidReference));

            RuleFor(x => x.FromDate).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);
        }
    }
}
