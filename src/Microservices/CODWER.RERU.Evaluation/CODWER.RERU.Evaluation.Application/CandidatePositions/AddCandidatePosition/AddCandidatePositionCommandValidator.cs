using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.AddCandidatePosition
{
    public class AddCandidatePositionCommandValidator : AbstractValidator<AddCandidatePositionCommand>
    {
        public AddCandidatePositionCommandValidator()
        {
            RuleFor(r => r.Data.Name)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_POSITION_NAME);

            RuleFor(r => r.Data)
                    .Must(x => x.To > x.From)
                    .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);
        }
    }
}
