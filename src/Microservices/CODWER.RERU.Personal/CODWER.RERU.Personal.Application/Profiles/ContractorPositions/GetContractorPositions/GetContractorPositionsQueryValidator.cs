using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorPositions.GetContractorPositions
{
    public class GetContractorPositionsQueryValidator : AbstractValidator<GetContractorPositionsQuery>
    {
        public GetContractorPositionsQueryValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            RuleFor(_ => new ContractorLocalPermission { GetPositionsData = true })
                .SetValidator(new LocalPermissionValidator(userProfileService, appDbContext));
        }
    }
}
