using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.Studies;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Studies.RemoveStudy
{
    public class RemoveStudyCommandValidator : AbstractValidator<RemoveStudyCommand>
    {
        public RemoveStudyCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Study>(appDbContext, ValidationCodes.STUDY_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
