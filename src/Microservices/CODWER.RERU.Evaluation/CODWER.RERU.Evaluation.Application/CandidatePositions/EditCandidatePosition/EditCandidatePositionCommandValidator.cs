using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.EditCandidatePosition
{
    public class EditCandidatePositionCommandValidator : AbstractValidator<EditCandidatePositionCommand>
    {
        public EditCandidatePositionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data.Name)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_POSITION_NAME);

            RuleFor(x => x.Data.Id)
              .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(appDbContext, ValidationCodes.INVALID_POSITION,
                  ValidationMessages.NotFound));

            RuleFor(r => r.Data)
                    .Must(x => x.To > x.From)
                    .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);

            RuleFor(r => r.Data.MedicalColumn)
               .NotNull()
               .WithErrorCode(ValidationCodes.EMPTY_MEDICAL_COLUMN);
        }
    }
}
