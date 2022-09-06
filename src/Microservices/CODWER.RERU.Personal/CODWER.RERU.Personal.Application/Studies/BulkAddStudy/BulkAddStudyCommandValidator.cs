using RERU.Data.Persistence.Context;
using FluentValidation;
using CODWER.RERU.Personal.Application.Validation;

namespace CODWER.RERU.Personal.Application.Studies.BulkAddStudy
{
    public class BulkAddStudyCommandValidator : AbstractValidator<BulkAddStudyCommand>
    {
        public BulkAddStudyCommandValidator(AppDbContext appDbContext)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new StudyValidator(appDbContext));

            RuleForEach(x => x.Data)
              .Must(x => x.YearOfAdmission < x.GraduationYear)
               .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);
        }
    }
}
