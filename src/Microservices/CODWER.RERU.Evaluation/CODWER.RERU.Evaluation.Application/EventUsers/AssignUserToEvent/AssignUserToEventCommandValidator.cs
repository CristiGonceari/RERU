using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventUsers.AssignUserToEvent
{
    public class AssignUserToEventCommandValidator : AbstractValidator<AssignUserToEventCommand>
    {
        private readonly AppDbContext _appDbContext;
        public AssignUserToEventCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext.NewInstance();

            RuleFor(r => r)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r != null, () =>
            {
                RuleFor(x => x.EventId)
                    .SetValidator(x => new ItemMustExistValidator<Event>(_appDbContext, ValidationCodes.INVALID_EVENT,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x)
                    .MustAsync((x, cancellation) => ExistentUser(x))
                    .WithErrorCode(ValidationCodes.INVALID_USER)
                    .WithMessage(ValidationMessages.InvalidReference);

                RuleFor(r => r)
                    .Must(x => !ExistentEvaluatorSameWithCandidate(x))
                    .WithErrorCode(ValidationCodes.EVALUATOR_AND_CANDIDATE_CANT_BE_THE_SAME);

                RuleFor(r => r)
                    .Must(x => !ExistentResponsiblePersonSameWithCandidate(x))
                    .WithErrorCode(ValidationCodes.CANDIDATE_AND_RESPONSIBLE_PERSON_CANT_BE_THE_SAME);

                RuleFor(r => r)
                    .Must(x => IsCandidateInUse(x))
                    .WithErrorCode(ValidationCodes.CANT_DELETE_CANDIDATE_IN_USE);
            });
        }

        private bool IsCandidateInUse(AssignUserToEventCommand data)
        {
            var candidatesIdsInUse = _appDbContext.Tests
                .Where(t => t.EventId == data.EventId)
                .Select(x => x.UserProfileId)
                .Distinct()
                .ToList();

            return Contains(candidatesIdsInUse, data.UserProfileId);
        }

        private bool Contains(List<int> list1, List<int> list2) => list1.Intersect(list2).Count() == list1.Count();

        private async Task<bool> ExistentUser(AssignUserToEventCommand data)
        {
            var listOfResults = new List<bool>();

            var db = _appDbContext.NewInstance();

            foreach (var userId in data.UserProfileId)
            {
                var result = db.UserProfiles.Any(up => up.Id == userId);

                listOfResults.Add(result);

            }

            if (listOfResults.Contains(false))
            {
                return false;
            }

            return true;
        }

        private bool ExistentEvaluatorSameWithCandidate(AssignUserToEventCommand data)
        {
            var listOfResults = new List<bool>();

            var db = _appDbContext.NewInstance();

            foreach (var userId in data.UserProfileId)
            {
                var result = db.EventEvaluators.Any(ev => ev.EvaluatorId == userId && ev.EventId == data.EventId);

                listOfResults.Add(result);
            }

            if (listOfResults.Contains(true))
            {
                return true;
            }

            return false;
        }

        private bool ExistentResponsiblePersonSameWithCandidate(AssignUserToEventCommand data)
        {
            var listOfResults = new List<bool>();

            var db = _appDbContext.NewInstance();

            foreach (var userId in data.UserProfileId)
            {
                var result = db.EventResponsiblePersons.Any(ev => ev.UserProfileId == userId && ev.EventId == data.EventId);

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
