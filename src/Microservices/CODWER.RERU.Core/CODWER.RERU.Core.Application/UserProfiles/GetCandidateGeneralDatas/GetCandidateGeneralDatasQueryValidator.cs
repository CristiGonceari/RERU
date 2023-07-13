using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.GetCandidateGeneralDatas
{
    public class GetCandidateGeneralDatasQueryValidator : AbstractValidator<GetCandidateGeneralDatasQuery>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;

        public GetCandidateGeneralDatasQueryValidator(ICurrentApplicationUserProvider currentUserProvider) 
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
