using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.Options.GetOption
{
    public class GetOptionQueryValidator : AbstractValidator<GetOptionQuery>
    {
        public GetOptionQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .Must(x => appDbContext.Options.Any(o => o.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_ID);
        }
    }
}
