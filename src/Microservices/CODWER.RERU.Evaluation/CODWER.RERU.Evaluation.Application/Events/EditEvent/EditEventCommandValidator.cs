using FluentValidation;
using System;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.EditEvent
{
    public class EditEventCommandValidator : AbstractValidator<EditEventCommand>
    {
        public EditEventCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.Id)
                    .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_NAME);

                RuleFor(r => r.Data.FromDate)
                    .GreaterThan(new DateTime(2000, 1, 1))
                    .WithErrorCode(ValidationCodes.INVALID_START_DATE);

                RuleFor(r => r.Data.TillDate)
                    .GreaterThan(new DateTime(2000, 1, 1))
                    .WithErrorCode(ValidationCodes.INVALID_END_DATE);

                RuleFor(r => r.Data)
                      .Must(x => x.TillDate > x.FromDate)
                      .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);

                RuleFor(r => r.Data)
                    .Must(x => x.FromDate >= DateTime.Today)
                    .WithErrorCode(ValidationCodes.START_DAY_CANT_BE_FROM_PAST);
            });
        }
    }
}
