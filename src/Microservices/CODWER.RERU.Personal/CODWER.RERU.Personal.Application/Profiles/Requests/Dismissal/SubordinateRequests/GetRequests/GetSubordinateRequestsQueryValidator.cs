using CODWER.RERU.Personal.Application.Services;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.SubordinateRequests.GetRequests
{
    public class GetSubordinateRequestsQueryValidator : AbstractValidator<GetSubordinateRequestsQuery>
    {
        public GetSubordinateRequestsQueryValidator(IUserProfileService userProfileService)
        {
            _ = userProfileService.GetCurrentContractorId().Result;
        }
    }
}
