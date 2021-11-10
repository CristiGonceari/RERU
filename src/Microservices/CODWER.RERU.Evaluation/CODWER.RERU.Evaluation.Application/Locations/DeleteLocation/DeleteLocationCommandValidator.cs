using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Locations.DeleteLocation
{
    public class DeleteLocationCommandValidator : AbstractValidator<DeleteLocationCommand>
    {
        public DeleteLocationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .Must(x => appDbContext.Locations.Any(l => l.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_LOCATION);
        }
    }
}
