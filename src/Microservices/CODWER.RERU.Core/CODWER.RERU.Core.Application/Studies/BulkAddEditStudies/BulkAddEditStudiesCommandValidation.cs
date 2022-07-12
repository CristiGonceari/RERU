using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Studies.BulkAddEditStudies
{
    public class BulkAddEditStudiesCommandValidation : AbstractValidator<BulkAddEditStudiesCommand>
    {
        public BulkAddEditStudiesCommandValidation(AppDbContext appDbContext)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new StudyValidator(appDbContext));
        }
    }
}
