using CVU.ERP.Common;
using RERU.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contractors.AddContractor
{
    public class AddContractorCommandValidator : AbstractValidator<AddContractorCommand>
    {
        public AddContractorCommandValidator(AppDbContext appDbContext, IDateTime dateTime)
        {
            RuleFor(x => x.Data).SetValidator(new ContractorValidator(appDbContext,dateTime));
        }
    }
}
