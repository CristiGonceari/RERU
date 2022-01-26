using CODWER.RERU.Personal.Application.Services;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.MyRequests.GetDismissRequest
{
    public class DismissalRequestQueryValidator : AbstractValidator<DismissalRequestQuery>
    {
        public DismissalRequestQueryValidator(IUserProfileService userProfileService)
        {
            _ = userProfileService.GetCurrentContractorId().Result;
        }
    }
}
