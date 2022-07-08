using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.AddSolicitedVacantPositionUserFile
{
    public class AddSolicitedVacantPositionUserFileCommandValidator : AbstractValidator<AddSolicitedVacantPositionUserFileCommand>
    {
        public AddSolicitedVacantPositionUserFileCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.UserProfileId)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                    ValidationMessages.NotFound));

            RuleFor(x => x.SolicitedVacantPositionId)
                .SetValidator(x => new ItemMustExistValidator<SolicitedVacantPosition>(appDbContext, ValidationCodes.INVALID_SOLICITED_VACANT_POSITION_ID,
                    ValidationMessages.NotFound));

            RuleFor(x => x.RequiredDocumentId)
                .SetValidator(x => new ItemMustExistValidator<RequiredDocument>(appDbContext, ValidationCodes.INVALID_SOLICITED_VACANT_POSITION_ID,
                    ValidationMessages.NotFound));
        }
    }
}
