using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.UnassignResponsiblePersonFromEvent
{
    public class UnassignResponsiblePersonFromEventCommandValidator : AbstractValidator<UnassignResponsiblePersonFromEventCommand>
    {
        public UnassignResponsiblePersonFromEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.UserProfileId)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x)
                .Must(x => appDbContext.EventResponsiblePersons.Any(l => l.EventId == x.EventId && l.UserProfileId == x.UserProfileId))
                .WithErrorCode(ValidationCodes.INVALID_RECORD);
        }
    }
}
