using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.CandidatePositions.EditCandidatePosition
{
    public class EditCandidatePositionCommandValidator : AbstractValidator<EditCandidatePositionCommand>
    {
        public EditCandidatePositionCommandValidator(AppDbContext coreDbContext)
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
