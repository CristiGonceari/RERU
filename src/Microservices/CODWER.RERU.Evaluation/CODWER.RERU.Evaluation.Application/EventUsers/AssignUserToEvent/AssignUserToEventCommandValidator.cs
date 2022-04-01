﻿using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.EventUsers.AssignUserToEvent
{
    public class AssignUserToEventCommandValidator : AbstractValidator<AssignUserToEventCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AssignUserToEventCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(r => r)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r != null, () =>
            {
                RuleFor(r => r)
                    .Must(x => !ExistentAssignedUser(x))
                    .WithErrorCode(ValidationCodes.USER_ALREADY_ASSIGNED);

                RuleFor(x => x.EventId)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
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
            });
        }
        private async Task<bool> ExistentUser(AssignUserToEventCommand data)
        {
            var listOfResults = new List<bool>();

            foreach (var userId in data.UserProfileId)
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

        private bool ExistentAssignedUser(AssignUserToEventCommand data)
        {
            var listOfResults = new List<bool>();


            foreach (var userId in data.UserProfileId)
            {
                var result = _appDbContext.EventUsers.Any(ev => ev.UserProfileId == userId && ev.EventId == data.EventId);

                listOfResults.Add(result);
            }

            if (listOfResults.Contains(true))
            {
                return true;
            }

            return false;
        }

        private bool ExistentEvaluatorSameWithCandidate(AssignUserToEventCommand data)
        {
            var listOfResults = new List<bool>();


            foreach (var userId in data.UserProfileId)
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

        private bool ExistentResponsiblePersonSameWithCandidate(AssignUserToEventCommand data)
        {
            var listOfResults = new List<bool>();


            foreach (var userId in data.UserProfileId)
            {
                var result = _appDbContext.EventResponsiblePersons.Any(ev => ev.UserProfileId == userId && ev.EventId == data.EventId);

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
