using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.Studies;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Studies.UpdateStudy
{
    public class UpdateStudyCommandValidator : AbstractValidator<UpdateStudyCommand>
    {
        public UpdateStudyCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Study>(appDbContext, ValidationCodes.STUDY_NOT_FOUND, ValidationMessages.NotFound));
           
            RuleFor(x => x.Data)
                .SetValidator(new StudyValidator(appDbContext));
        }
    }
}
