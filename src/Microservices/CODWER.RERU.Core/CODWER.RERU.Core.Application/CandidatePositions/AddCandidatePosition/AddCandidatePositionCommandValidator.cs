using CODWER.RERU.Core.Application.Validation;
using FluentValidation;

namespace CODWER.RERU.Core.Application.CandidatePositions.AddCandidatePosition
{
    public class AddCandidatePositionCommandValidator : AbstractValidator<AddCandidatePositionCommand>
    {
        public AddCandidatePositionCommandValidator()
        {
            RuleFor(r => r.Data.Name)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_POSITION_NAME);
        }
    }
}
