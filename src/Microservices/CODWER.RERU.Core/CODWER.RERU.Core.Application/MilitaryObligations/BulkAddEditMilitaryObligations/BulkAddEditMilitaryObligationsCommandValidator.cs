using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.MilitaryObligations.BulkAddEditMilitaryObligations
{
    public class BulkAddEditMilitaryObligationsCommandValidator : AbstractValidator<BulkAddEditMilitaryObligationsCommand>
    {
        public BulkAddEditMilitaryObligationsCommandValidator(AppDbContext appDbContext)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new MilitaryObligationValidator(appDbContext));
        }
    }
}
