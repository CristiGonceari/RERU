using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Studies.BulkAddEditStudies
{
    public class BulkAddEditStudiesCommandValidation : AbstractValidator<BulkAddEditStudiesCommand>
    {
        public BulkAddEditStudiesCommandValidation(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new StudyValidator(appDbContext, currentUserProvider));

            //RuleForEach(x => x.Data)
            //   .Must(x => x.YearOfAdmission < x.GraduationYear)
            //    .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);
        }
    }
}
