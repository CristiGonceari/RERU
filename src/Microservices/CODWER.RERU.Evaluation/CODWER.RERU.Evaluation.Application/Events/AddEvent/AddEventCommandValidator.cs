using FluentValidation;
using System;
using CODWER.RERU.Evaluation.Application.Validation;

namespace CODWER.RERU.Evaluation.Application.Events.AddEvent
{
    public class AddEventCommandValidator : AbstractValidator<AddEventCommand>
    {
        public AddEventCommandValidator()
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
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
            });
        }
    }
}
