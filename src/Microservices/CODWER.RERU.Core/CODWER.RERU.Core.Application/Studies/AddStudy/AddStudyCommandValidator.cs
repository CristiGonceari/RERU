using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Studies.AddStudy
{
    public class AddStudyCommandValidator : AbstractValidator<AddStudyCommand>
    {
        public AddStudyCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new StudyValidator(appDbContext));
        }
    }
}
