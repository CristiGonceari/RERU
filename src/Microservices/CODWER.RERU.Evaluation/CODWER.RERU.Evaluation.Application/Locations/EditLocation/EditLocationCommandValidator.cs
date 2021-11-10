using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Locations.EditLocation
{
    public class EditLocationCommandValidator : AbstractValidator<EditLocationCommand>
    {
        public EditLocationCommandValidator(AppDbContext appDbContext)
        {
            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.Id)
                    .NotNull()
                    .Must(x => appDbContext.Locations.Any(u => u.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_LOCATION);

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
