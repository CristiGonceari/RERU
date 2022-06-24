using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Studies.RemoveStudy
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
