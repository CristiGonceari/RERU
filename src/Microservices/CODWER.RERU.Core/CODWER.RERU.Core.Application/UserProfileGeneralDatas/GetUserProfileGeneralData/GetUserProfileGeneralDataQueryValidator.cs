using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas.GetUserProfileGeneralData
{
    public class GetUserProfileGeneralDataQueryValidator : AbstractValidator<GetUserProfileGeneralDataQuery>
    {
        private readonly AppDbContext _appDbContext;
        public GetUserProfileGeneralDataQueryValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.UserProfileId)
                .SetValidator(new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));

        }
    }
}
