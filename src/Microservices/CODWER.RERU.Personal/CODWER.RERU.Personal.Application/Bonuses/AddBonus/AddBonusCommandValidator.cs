using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Bonuses.AddBonus
{
    public class AddBonusCommandValidator : AbstractValidator<AddBonusCommand>
    {
        public AddBonusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new BonusValidator(appDbContext));
        }
    }
}
