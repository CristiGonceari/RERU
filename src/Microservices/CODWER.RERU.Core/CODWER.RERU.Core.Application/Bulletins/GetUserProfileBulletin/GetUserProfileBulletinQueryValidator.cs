using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Core.Application.Bulletins.GetUserProfileBulletin
{
    public class GetUserProfileBulletinQueryValidator : AbstractValidator<GetUserProfileBulletinQuery>
    {
        private readonly AppDbContext _appDbContext;
        public GetUserProfileBulletinQueryValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.UserProfileId).Custom(ValidateUserProfileBulletin);

        }
        private void ValidateUserProfileBulletin(int userProfileId, CustomContext context)
        {
            var existent = _appDbContext.Bulletins.FirstOrDefault(x => x.UserProfileId == userProfileId);

            if (existent == null)
            {
                context.AddFail(ValidationCodes.BULLETIN_NOT_FOUND, ValidationMessages.NotFound);
            }
        }
    }
}
