using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.AddContractorDepartment
{
    public class AddContractorDepartmentCommandValidator : AbstractValidator<AddContractorDepartmentCommand>
    {
        public AddContractorDepartmentCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new ContractorDepartmentValidator(appDbContext));
        }
    }
}
