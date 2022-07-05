using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.GetKinshipRelationWithUserProfiles
{
    public class GetKinshipRelationWithUserProfilesQueryValidator : AbstractValidator<GetKinshipRelationWithUserProfilesQuery>
    {
        public GetKinshipRelationWithUserProfilesQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.UserProfileId)
                .SetValidator(new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));
        }
    }
}
