using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.UnassignEvaluatorFromEvent
{
    public class UnassignEvaluatorFromEventCommandValidator : AbstractValidator<UnassignEvaluatorFromEventCommand>
    {
        public UnassignEvaluatorFromEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.EvaluatorId)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x)
                .Must(x => appDbContext.EventEvaluators.Any(e => e.EventId == x.EventId && e.EvaluatorId == x.EvaluatorId))
                .WithErrorCode(ValidationCodes.INVALID_RECORD);
        }
    }
}
