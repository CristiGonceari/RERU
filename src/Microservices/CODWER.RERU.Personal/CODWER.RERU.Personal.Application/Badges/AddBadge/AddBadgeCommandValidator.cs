using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Badges.AddBadge
{
    public class AddBadgeCommandValidator : AbstractValidator<AddBadgeCommand>
    {
        public AddBadgeCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new BadgeValidator(appDbContext));
        }
    }
}
