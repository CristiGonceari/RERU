using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.KinshipRelations.BulkAddEditKinshipRelations
{
    public class BulkAddEditKinshipRelationsCommandValidator : AbstractValidator<BulkAddEditKinshipRelationsCommand>
    {
        public BulkAddEditKinshipRelationsCommandValidator(AppDbContext appDbContext)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new KinshipRelationValidator(appDbContext));
        }
    }
}
