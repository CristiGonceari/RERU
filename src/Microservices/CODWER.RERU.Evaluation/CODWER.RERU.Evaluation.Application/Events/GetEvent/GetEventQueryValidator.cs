using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.Events.GetEvent
{
    public class GetEventQueryValidator : AbstractValidator<GetEventQuery>
    {
        public GetEventQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .Must(x => appDbContext.Events.Any(e => e.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_ID);
        }
    }
}
