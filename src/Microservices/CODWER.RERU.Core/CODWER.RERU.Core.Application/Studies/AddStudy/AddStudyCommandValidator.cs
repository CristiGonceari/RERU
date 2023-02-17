using CODWER.RERU.Core.Application.Validation;
using FluentValidation;
using RERU.Data.Persistence.Context;
using System;

namespace CODWER.RERU.Core.Application.Studies.AddStudy
{
    public class AddStudyCommandValidator : AbstractValidator<AddStudyCommand>
    {
        public AddStudyCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new StudyValidator(appDbContext));

            RuleFor(x => x.Data)
              .Must(x => Int64.Parse(x.YearOfAdmission) < Int64.Parse(x.GraduationYear))
               .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);
        }
    }
}
