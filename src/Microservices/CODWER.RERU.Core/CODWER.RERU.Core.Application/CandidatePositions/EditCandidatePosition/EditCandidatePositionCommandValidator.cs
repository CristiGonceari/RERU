using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Core.Application.CandidatePositions.EditCandidatePosition
{
    public class EditCandidatePositionCommandValidator : AbstractValidator<EditCandidatePositionCommand>
    {
        public EditCandidatePositionCommandValidator(CoreDbContext coreDbContext)
        {
            RuleFor(r => r.Data.Name)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_POSITION_NAME);

            RuleFor(x => x.Data.Id)
              .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(coreDbContext, ValidationCodes.INVALID_POSITION,
                  ValidationMessages.NotFound));
            
        }
    }
}
