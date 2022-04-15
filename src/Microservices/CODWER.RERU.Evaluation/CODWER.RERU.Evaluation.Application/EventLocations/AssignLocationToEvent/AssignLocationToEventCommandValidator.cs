using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventLocations.AssignLocationToEvent
{
    public class AssignLocationToEventCommandValidator : AbstractValidator<AssignLocationToEventCommand>
    {
        public AssignLocationToEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(r => r.Data.LocationId)
                    .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_LOCATION,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data.EventId)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data)
                    .Must(x => !appDbContext.EventLocations.Any(l => l.LocationId == x.LocationId && l.EventId == x.EventId))
                    .WithErrorCode(ValidationCodes.EXISTENT_LOCATION_IN_EVENT);
            });
        }
    }
}
