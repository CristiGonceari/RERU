using RERU.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Ranks.AddRank
{
    public class AddRankCommandValidator : AbstractValidator<AddRankCommand>
    {
        public AddRankCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new RankValidator(appDbContext));
        }
    }
}
