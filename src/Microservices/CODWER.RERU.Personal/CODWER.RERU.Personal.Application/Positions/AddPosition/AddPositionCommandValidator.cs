using RERU.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Positions.AddPosition
{
    public class AddPositionCommandValidator : AbstractValidator<AddPositionCommand>
    {
        public AddPositionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new PositionValidator(appDbContext));
        }
    }
}
