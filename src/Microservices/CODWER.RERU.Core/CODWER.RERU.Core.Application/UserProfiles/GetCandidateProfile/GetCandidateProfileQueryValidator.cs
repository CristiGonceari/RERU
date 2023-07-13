using CODWER.RERU.Core.Application.UserProfiles.GetCandidateGeneralDatas;
using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.GetCandidateProfile
{
    public class GetCandidateProfileQueryValidator : AbstractValidator<GetCandidateProfileQuery>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;

        public GetCandidateProfileQueryValidator(ICurrentApplicationUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;

            RuleFor(x => x)
               .Must(x => CheckIfCurrentUser(x.Id).Result)
               .WithErrorCode(ValidationCodes.USER_NOT_FOUND);
        }
        private async Task<bool> CheckIfCurrentUser(int id)
        {
            var currentUser = await _currentUserProvider.Get();

            if (currentUser.Id == id.ToString())
            {
                return true;
            }

            return false;
        }
    }
}
