using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Penalizations.AddPenalization
{
    public class AddPenalizationCommandValidator : AbstractValidator<AddPenalizationCommand>
    {
        public AddPenalizationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new PenalizationValidator(appDbContext));
        }
    }
}
