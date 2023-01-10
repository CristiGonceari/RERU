using CODWER.RERU.Core.Application.Validation;
using FluentValidation;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Core.Application.Users.GetUserDetailsByEmail
{
    public class GetUserDetailsByEmailQueryValidator : AbstractValidator<GetUserDetailsByEmailQuery>
    {
        private readonly AppDbContext _appDbContext;

        public GetUserDetailsByEmailQueryValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x)
               .Must(x => CheckExistentEmail(x.Email))
               .WithErrorCode(ValidationCodes.INVALID_EMAIL_FORMAT);
        }

        private bool CheckExistentEmail(string email)
        {
            var userProfile = _appDbContext.UserProfiles.FirstOrDefault(x => x.Email == email);

            if (userProfile != null)
            {
                if (userProfile.Email == email)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
