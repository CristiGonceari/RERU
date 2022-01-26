using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorBulletin.GetContractorBulletin
{
    public class GetContractorBulletinQueryValidator : AbstractValidator<GetContractorBulletinQuery>
    {
        public GetContractorBulletinQueryValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            RuleFor(_ => new ContractorLocalPermission { GetBulletinData = true })
                .SetValidator(new LocalPermissionValidator(userProfileService, appDbContext));
        }
    }
}
