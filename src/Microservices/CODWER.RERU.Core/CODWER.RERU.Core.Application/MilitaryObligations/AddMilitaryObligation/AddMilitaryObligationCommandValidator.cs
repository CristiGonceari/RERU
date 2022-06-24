using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.MilitaryObligations.AddMilitaryObligation
{
    public class AddMilitaryObligationCommandValidator : AbstractValidator<AddMilitaryObligationCommand>
    {
        public AddMilitaryObligationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new MilitaryObligationValidator(appDbContext));
        }
    }
}
 