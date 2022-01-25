using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contractors.AddContractor
{
    public class AddContractorCommandValidator : AbstractValidator<AddContractorCommand>
    {
        public AddContractorCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new ContractorValidator(appDbContext));
        }
    }
}
