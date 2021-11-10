using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.AssignEvaluatorToEvent
{
    public class AssignEvaluatorToEventCommandValidator : AbstractValidator<AssignEvaluatorToEventCommand>
    {
        public AssignEvaluatorToEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.EventId)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.EvaluatorId)
                    .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.EventEvaluators.Any(e => e.EventId == x.EventId && e.EvaluatorId == x.EvaluatorId))
                    .WithErrorCode(ValidationCodes.USER_ALREADY_ASSIGNED);

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.EventUsers.Any(e => e.EventId == x.EventId && e.UserProfileId == x.EvaluatorId))
                    .WithErrorCode(ValidationCodes.EVALUATOR_AND_CANDIDATE_CANT_BE_THE_SAME);

                RuleFor(r => r.Data)
                    .Must(x => appDbContext.Tests.Where(t => t.EventId == x.EventId).All(t => t.EvaluatorId == null))
                    .WithErrorCode(ValidationCodes.EXISTENT_EVALUATOR_IN_EVENT);
            });
        }
    }
}
