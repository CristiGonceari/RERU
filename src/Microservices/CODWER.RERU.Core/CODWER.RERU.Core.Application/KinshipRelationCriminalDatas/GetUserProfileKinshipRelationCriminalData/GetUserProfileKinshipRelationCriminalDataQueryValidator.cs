using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.GetUserProfileKinshipRelationCriminalData
{
    public class GetUserProfileKinshipRelationCriminalDataQueryValidator : AbstractValidator<GetUserProfileKinshipRelationCriminalDataQuery>
    {
        private readonly AppDbContext _appDbContext;
        public GetUserProfileKinshipRelationCriminalDataQueryValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.UserProfileId)
                .SetValidator(new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));

        }
    }
}
