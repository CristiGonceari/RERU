using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorRanks.GetContractorRanks
{
    public class GetContractorRanksQueryValidator : AbstractValidator<GetContractorRanksQuery>
    {
        public GetContractorRanksQueryValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            RuleFor(_ => new ContractorLocalPermission { GetRanksData = true })
                .SetValidator(new LocalPermissionValidator(userProfileService, appDbContext));
        }
    }
}
