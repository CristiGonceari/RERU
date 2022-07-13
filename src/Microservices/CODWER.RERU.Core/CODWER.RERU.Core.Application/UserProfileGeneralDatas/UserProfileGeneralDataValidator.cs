using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.UserProfileGeneralDatas;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.UserProfileGeneralDatas
{
    public class UserProfileGeneralDataValidator : AbstractValidator<UserProfileGeneralDataDto>
    {
        public UserProfileGeneralDataValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.CandidateCitizenshipId)
             .SetValidator(new ItemMustExistValidator<CandidateCitizenship>(appDbContext, ValidationCodes.CANDIDATE_CITIZENSHIP_NOT_FOUND,
                 ValidationMessages.InvalidReference));

            RuleFor(x => x.CandidateNationalityId)
             .SetValidator(new ItemMustExistValidator<CandidateNationality>(appDbContext, ValidationCodes.CANDIDATE_NATIONALITY_NOT_FOUND,
                 ValidationMessages.InvalidReference));

            RuleFor(x => x.WorkPhone)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_USER_WORK_PHONE)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.HomePhone)
               .NotEmpty()
               .WithErrorCode(ValidationCodes.EMPTY_USER_HOME_PHONE)
               .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => (int)x.Sex)
               .SetValidator(new ExistInEnumValidator<SexTypeEnum>());

            RuleFor(x => (int)x.StateLanguageLevel)
               .SetValidator(new ExistInEnumValidator<StateLanguageLevel>());
        }
    }
}
