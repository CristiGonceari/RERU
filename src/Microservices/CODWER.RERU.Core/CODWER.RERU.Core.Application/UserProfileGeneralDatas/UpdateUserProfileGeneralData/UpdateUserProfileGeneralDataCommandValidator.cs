using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas.UpdateUserProfileGeneralData
{
    public class UpdateUserProfileGeneralDataCommandValidator : AbstractValidator<UpdateUserProfileGeneralDataCommand>
    {
        private readonly AppDbContext _appDbContext;

        public UpdateUserProfileGeneralDataCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<UserProfileGeneralData>(appDbContext, ValidationCodes.USER_PROFILE_GENERAL_DATA_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data)
                .SetValidator(new UserProfileGeneralDataValidator(appDbContext));
        }
    }
}
