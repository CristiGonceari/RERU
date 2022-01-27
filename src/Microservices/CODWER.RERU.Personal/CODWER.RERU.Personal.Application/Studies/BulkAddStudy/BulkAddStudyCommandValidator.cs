using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Studies.BulkAddStudy
{
    public class BulkAddStudyCommandValidator : AbstractValidator<BulkAddStudyCommand>
    {
        public BulkAddStudyCommandValidator(AppDbContext appDbContext)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new StudyValidator(appDbContext));
        }
    }
}
