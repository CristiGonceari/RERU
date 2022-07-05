using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.MaterialStatuses.GetUserProfilesMaterialStatus
{
    public class GetUserProfilesMaterialStatusQueryValidator : AbstractValidator<GetUserProfilesMaterialStatusQuery>
    {
        public GetUserProfilesMaterialStatusQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.UserProfileId)
               .SetValidator(new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                   ValidationMessages.InvalidReference));
        }
    }
}
