using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.EditCandidate
{
    public class EditCandidateCommandValidator : AbstractValidator<EditCandidateCommand>
    {
        private readonly AppDbContext _appDbContext;
        public EditCandidateCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data.Id)
                .SetValidator(x => new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.CandidateCitizenshipId)
                .SetValidator(x => new ItemMustExistValidator<CandidateCitizenship>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

             RuleFor(x => x.Data.CandidateNationalityId)
                .SetValidator(x => new ItemMustExistValidator<CandidateNationality>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.WorkPhone).NotEmpty()
               .WithMessage(ValidationMessages.InvalidInput)
               .WithErrorCode(ValidationCodes.EMPTY_USER_WORK_PHONE);

            RuleFor(x => x.Data.HomePhone).NotEmpty()
               .WithMessage(ValidationMessages.InvalidInput)
               .WithErrorCode(ValidationCodes.EMPTY_USER_HOME_PHONE);

        }
    }
}
