using RERU.Data.Persistence.Context;
using FluentValidation;
using CODWER.RERU.Personal.Application.Validation;

namespace CODWER.RERU.Personal.Application.Studies.AddStudy
{
    public class CreateStudyCommandValidator : AbstractValidator<CreateStudyCommand>
    {
        public CreateStudyCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new StudyValidator(appDbContext));

            RuleFor(x => x.Data)
              .Must(x => x.YearOfAdmission < x.GraduationYear)
               .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);
        }
    }
}
