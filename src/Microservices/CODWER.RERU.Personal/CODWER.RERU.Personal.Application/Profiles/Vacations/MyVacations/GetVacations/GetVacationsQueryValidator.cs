using CODWER.RERU.Personal.Application.Services;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetVacations
{
    public class GetVacationsQueryValidator : AbstractValidator<GetVacationsQuery>
    {
        public GetVacationsQueryValidator(IUserProfileService userProfileService)
        {
            _ = userProfileService.GetCurrentContractorId().Result;
        }
    }
}
