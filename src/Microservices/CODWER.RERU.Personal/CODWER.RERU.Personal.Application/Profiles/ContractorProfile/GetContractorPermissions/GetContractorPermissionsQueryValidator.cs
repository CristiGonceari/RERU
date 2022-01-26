using CODWER.RERU.Personal.Application.Services;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorProfile.GetContractorPermissions
{
    public class GetContractorPermissionsQueryValidator : AbstractValidator<GetContractorPermissionsQuery>
    {
        public GetContractorPermissionsQueryValidator(IUserProfileService userProfileService)
        {
            _ = userProfileService.GetCurrentContractorId().Result;
        }
    }
}
