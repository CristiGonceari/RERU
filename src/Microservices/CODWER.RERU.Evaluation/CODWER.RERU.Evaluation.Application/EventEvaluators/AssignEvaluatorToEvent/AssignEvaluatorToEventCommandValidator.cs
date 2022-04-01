using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.AssignEvaluatorToEvent
{
    public class AssignEvaluatorToEventCommandValidator : AbstractValidator<AssignEvaluatorToEventCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AssignEvaluatorToEventCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(r => r)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r != null, () =>
            {
                RuleFor(x => x.EventId)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x)
                    .MustAsync((x, cancellation) => ExistentUser(x))
                    .WithErrorCode(ValidationCodes.INVALID_USER)
                    .WithMessage(ValidationMessages.InvalidReference);

                RuleFor(r => r)
                    .Must(r => !ExistentAssignedEvaluator(r))
                    .WithErrorCode(ValidationCodes.USER_ALREADY_ASSIGNED);

                RuleFor(r => r)
                    .Must(r => !ExistentEvaluatorSameWithCandidate(r))
                    .WithErrorCode(ValidationCodes.EVALUATOR_AND_CANDIDATE_CANT_BE_THE_SAME);

                RuleFor(r => r)
                    .Must(x => appDbContext.Tests.Where(t => t.EventId == x.EventId).All(t => t.EvaluatorId == null))
                    .WithErrorCode(ValidationCodes.EXISTENT_EVALUATOR_IN_EVENT);
            });
        }
        private async Task<bool> ExistentUser(AssignEvaluatorToEventCommand data)
        {
            var listOfResults = new List<bool>();

            foreach (var userId in data.EvaluatorId)
            {
                var result = _appDbContext.UserProfiles.Any(up => up.Id == userId);

                listOfResults.Add(result);

            }

            if (listOfResults.Contains(false))
            {
                return false;
            }

            return true;
        }

        private bool ExistentAssignedEvaluator(AssignEvaluatorToEventCommand data)
        {
            var listOfResults = new List<bool>();


            foreach (var userId in data.EvaluatorId)
            {
                var result = _appDbContext.EventEvaluators.Any(ev => ev.EvaluatorId == userId && ev.EventId == data.EventId);

                listOfResults.Add(result);
            }

            if (listOfResults.Contains(true))
            {
                return true;
            }

            return false;
        }

        private bool ExistentEvaluatorSameWithCandidate(AssignEvaluatorToEventCommand data)
        {
            var listOfResults = new List<bool>();

            foreach (var userId in data.EvaluatorId)
            {
                var result = _appDbContext.EventUsers.Any(eu => eu.EventId == data.EventId && eu.UserProfileId == userId);

                listOfResults.Add(result);
            }

            if (listOfResults.Contains(true))
            {
                return true;
            }

            return false;
        }

    }
}
