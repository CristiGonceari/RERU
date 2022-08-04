using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.GetContractorDepartment
{
    public class GetContractorDepartmentQueryValidator : AbstractValidator<GetContractorDepartmentQuery>
    {
        public GetContractorDepartmentQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<ContractorDepartment>(appDbContext,ValidationCodes.CONTRACTOR_DEPARTMENT_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
