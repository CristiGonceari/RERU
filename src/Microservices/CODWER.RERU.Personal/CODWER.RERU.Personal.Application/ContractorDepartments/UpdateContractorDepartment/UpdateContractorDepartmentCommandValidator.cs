using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.UpdateContractorDepartment
{
    public class UpdateContractorDepartmentCommandValidator : AbstractValidator<UpdateContractorDepartmentCommand>
    {
        public UpdateContractorDepartmentCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<ContractorDepartment>(appDbContext, ValidationCodes.CONTRACTOR_DEPARTMENT_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new ContractorDepartmentValidator(appDbContext));
        }
    }
}
