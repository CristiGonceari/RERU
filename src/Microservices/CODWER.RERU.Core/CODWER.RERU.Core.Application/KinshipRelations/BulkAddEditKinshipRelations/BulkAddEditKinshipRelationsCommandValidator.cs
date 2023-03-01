using CVU.ERP.Common;
using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.KinshipRelations.BulkAddEditKinshipRelations
{
    public class BulkAddEditKinshipRelationsCommandValidator : AbstractValidator<BulkAddEditKinshipRelationsCommand>
    {
        public BulkAddEditKinshipRelationsCommandValidator(AppDbContext appDbContext, IDateTime dateTime)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new KinshipRelationValidator(appDbContext, dateTime));
        }
    }
}
