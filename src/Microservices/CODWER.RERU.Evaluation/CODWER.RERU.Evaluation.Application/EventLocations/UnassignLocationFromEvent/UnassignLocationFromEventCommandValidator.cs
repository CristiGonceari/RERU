using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventLocations.UnassignLocationFromEvent
{
    public class UnassignLocationFromEventCommandValidator : AbstractValidator<UnassignLocationFromEventCommand>
    {
        public UnassignLocationFromEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x)
                .Must(x => appDbContext.EventLocations.Any(l => l.LocationId == x.LocationId && l.EventId == x.EventId))
                .WithErrorCode(ValidationCodes.INVALID_RECORD);

            RuleFor(x => x.EventId)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.LocationId)
                .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_LOCATION,
                    ValidationMessages.InvalidReference));
        }
    }
}
