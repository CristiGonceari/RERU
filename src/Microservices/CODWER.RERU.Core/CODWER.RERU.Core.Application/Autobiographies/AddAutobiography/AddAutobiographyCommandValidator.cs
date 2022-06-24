using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Autobiographies.AddAutobiography
{
    public class AddAutobiographyCommandValidator : AbstractValidator<AddAutobiographyCommand>
    {
        public AddAutobiographyCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data)
               .SetValidator(new AutobiographyValidator(appDbContext));
        }
    }
}
