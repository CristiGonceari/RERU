using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.VacationConfigurations.GetContractorAvailableVacations
{
    public class GetContractorAvailableVacationsQueryValidator : AbstractValidator<GetContractorAvailableVacationsQuery>
    {
        public GetContractorAvailableVacationsQueryValidator(AppDbContext appDbContext)
        {
            //RuleFor(x => x.ContractorId)
            //    .SetValidator(new ExistentContractorValidator(appDbContext, ValidationMessages.InvalidReference));
        }
    }
}
