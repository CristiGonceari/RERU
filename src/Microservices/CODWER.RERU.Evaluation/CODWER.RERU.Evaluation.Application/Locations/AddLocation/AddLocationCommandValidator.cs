using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.Locations.AddLocation
{
    public class AddLocationCommandValidator : AbstractValidator<AddLocationCommand>
    {
        public AddLocationCommandValidator()
        {
            When(r => r.Data != null, () =>
            {
                RuleFor(r => r.Data.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_LOCATION_NAME);

                RuleFor(r => r.Data.Address)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_ADDRESS);

                RuleFor(r => r.Data.Places)
                    .NotNull()
                    .GreaterThan(0)
                    .WithErrorCode(ValidationCodes.EMPTY_COMPUTERS_COUNT);
            });
        }
    }
}
