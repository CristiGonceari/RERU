using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.AssignResponsiblePersonToEvent
{
    public class AssignResponsiblePersonToEventCommandValidator : AbstractValidator<AssignResponsiblePersonToEventCommand>
    {
        public AssignResponsiblePersonToEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.EventId)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.UserProfileId)
                    .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.EventResponsiblePersons.Any(l => l.EventId == x.EventId && l.UserProfileId == x.UserProfileId))
                    .WithErrorCode(ValidationCodes.USER_ALREADY_ASSIGNED);

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.EventUsers.Any(e => e.EventId == x.EventId && e.UserProfileId == x.UserProfileId))
                    .WithErrorCode(ValidationCodes.CANDIDATE_AND_RESPONSIBLE_PERSON_CANT_BE_THE_SAME);
            });
        }
    }
}
