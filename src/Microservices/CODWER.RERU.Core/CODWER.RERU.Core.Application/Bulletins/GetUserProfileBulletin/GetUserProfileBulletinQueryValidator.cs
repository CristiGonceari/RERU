using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
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

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));

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
