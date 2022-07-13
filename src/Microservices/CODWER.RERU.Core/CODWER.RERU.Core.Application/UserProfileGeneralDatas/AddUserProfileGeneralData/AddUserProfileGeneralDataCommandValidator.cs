using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas.AddUserProfileGeneralData
{
    public class AddUserProfileGeneralDataCommandValidator : AbstractValidator<AddUserProfileGeneralDataCommand>
    {
        public readonly AppDbContext _appDbContext;

        public AddUserProfileGeneralDataCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data)
                .SetValidator(new UserProfileGeneralDataValidator(appDbContext));

            RuleFor(x => x.Data.UserProfileId)
                .Custom(CheckIfContractorHasGeneralData);
        }

        private void CheckIfContractorHasGeneralData(int userProfileId, CustomContext context)
        {
            var exist = _appDbContext.UserProfileGeneralDatas.Any(x => x.UserProfileId == userProfileId);

            if (exist)
            {
                context.AddFail(ValidationCodes.CONTRACTOR_HAS_GENERAL_DATA, ValidationMessages.InvalidReference);
            }
        }
    }
}
