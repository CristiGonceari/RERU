using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Studies.BulkAddStudies
{
    public class BulkAddStudiesCommandValidation : AbstractValidator<BulkAddStudiesCommand>
    {
        public BulkAddStudiesCommandValidation(AppDbContext appDbContext)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new StudyValidator(appDbContext));
        }
    }
}
