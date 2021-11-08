using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Options.AddOption
{
    public class AddOptionCommandValidator : AbstractValidator<AddOptionCommand>
    {
        public AddOptionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Input.QuestionUnitId)
                .Must(x => appDbContext.QuestionUnits.Any(d => d.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_QUESTION);

            RuleFor(r => r.Input.Answer)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_ANSWER);

            RuleFor(r => r.Input.IsCorrect)
                .NotNull()
                .WithErrorCode(ValidationCodes.EMPTY_CORRECT_ANSWER);
        }
    }
}
