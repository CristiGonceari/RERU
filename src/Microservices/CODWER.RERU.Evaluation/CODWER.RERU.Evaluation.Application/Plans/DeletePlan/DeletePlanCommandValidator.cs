using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Plans.DeletePlan
{
    public class DeletePlanCommandValidator : AbstractValidator<DeletePlanCommand>
    {
        public DeletePlanCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .Must(x => appDbContext.Plans.Any(l => l.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_PLAN);
        }
    }

}
