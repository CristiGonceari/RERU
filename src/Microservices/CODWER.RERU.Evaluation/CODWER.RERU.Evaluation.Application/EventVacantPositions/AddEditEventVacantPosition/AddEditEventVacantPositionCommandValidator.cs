using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions.AddEditEventVacantPosition
{
    public class AddEditEventVacantPositionCommandValidator : AbstractValidator<AddEditEventVacantPositionCommand>
    {
        public AddEditEventVacantPositionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.CandidatePositionId)
                    .SetValidator(new ItemMustExistValidator<CandidatePosition>(appDbContext, ValidationCodes.INVALID_POSITION, ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.EventId)
                    .SetValidator(new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_REQUIRED_DOCUMENT, ValidationMessages.InvalidReference));
            });
        }
    }
}
