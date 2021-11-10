using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.DeleteEvent
{
    public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .Must(x => appDbContext.Events.Any(l => l.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_EVENT);
        }
    }
}
