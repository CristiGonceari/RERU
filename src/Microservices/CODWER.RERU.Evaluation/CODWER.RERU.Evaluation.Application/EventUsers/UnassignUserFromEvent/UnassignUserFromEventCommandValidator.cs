using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.EventUsers.UnassignUserFromEvent
{
    public class UnassignUserFromEventCommandValidator : AbstractValidator<UnassignUserFromEventCommand>
    {
        public UnassignUserFromEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.UserProfileId)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x)
                .Must(x => appDbContext.EventUsers.Any(l => l.EventId == x.EventId && l.UserProfileId == x.UserProfileId))
                .WithErrorCode(ValidationCodes.INVALID_RECORD);
        }
    }
}
