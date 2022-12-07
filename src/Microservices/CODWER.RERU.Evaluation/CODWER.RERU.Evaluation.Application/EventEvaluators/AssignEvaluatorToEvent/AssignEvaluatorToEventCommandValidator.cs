using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using System.Threading.Tasks;
using System.Collections.Generic;
using Minio.DataModel;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

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
                    .Must(r => !ExistentEvaluatorSameWithCandidate(r))
                    .WithErrorCode(ValidationCodes.EVALUATOR_AND_CANDIDATE_CANT_BE_THE_SAME);

                RuleFor(r => r)
                    .Must(x => IsEvaluatorInUse(x))
                    .WithErrorCode(ValidationCodes.CANT_DELETE_EVALUATOR_IN_USE);
            });
        }

        private bool IsEvaluatorInUse(AssignEvaluatorToEventCommand data)
        {
            var evaluatorsIdsInUse = _appDbContext.Tests
                .Where(t => t.EventId == data.EventId)
                .Select(x => x.EvaluatorId)
                .Distinct()
                .ToList();

            var list = evaluatorsIdsInUse.Where(x => x != null).Select(x => x.Value).ToList();

            return Contains(list, data.EvaluatorId);
        }

        private bool Contains(List<int> list1, List<int> list2) => list1.Intersect(list2).Count() == list1.Count();

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
