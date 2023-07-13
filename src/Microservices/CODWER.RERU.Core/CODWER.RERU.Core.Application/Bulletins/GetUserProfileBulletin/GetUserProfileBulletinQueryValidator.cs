using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Bulletins.GetUserProfileBulletin
{
    public class GetUserProfileBulletinQueryValidator : AbstractValidator<GetUserProfileBulletinQuery>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;

        public GetUserProfileBulletinQueryValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _appDbContext = appDbContext;

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x)
               .Must(x => CheckIfCurrentUser(x.ContractorId).Result)
               .WithErrorCode(ValidationCodes.USER_NOT_FOUND);
        }
        private async Task<bool> CheckIfCurrentUser(int id)
        {
            var currentUser = await _currentUserProvider.Get();
            var contractor = _appDbContext.Contractors.Include(up => up.UserProfile).FirstOrDefault(up => up.UserProfileId.ToString() == currentUser.Id);

            if (contractor.Id == id)
            {
                return true;
            }

            return false;
        }
        //private void ValidateUserProfileBulletin(int userProfileId, CustomContext context)
        //{
        //    var existent = _appDbContext.Bulletins.FirstOrDefault(x => x.UserProfileId == userProfileId);

        //    if (existent == null)
        //    {
        //        context.AddFail(ValidationCodes.BULLETIN_NOT_FOUND, ValidationMessages.NotFound);
        //    }
        //}
    }
}
