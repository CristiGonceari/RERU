using CODWER.RERU.Personal.Application.Services;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.SubordinatesVacations.GetVacations
{
    public class GetSubordinateVacationsQueryValidator : AbstractValidator<GetSubordinateVacationsQuery>
    {
        public GetSubordinateVacationsQueryValidator(IUserProfileService userProfileService)
        {
            _ = userProfileService.GetCurrentContractorId().Result;
        }
    }
}
