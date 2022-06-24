using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.UpdateUserProfileModernLanguageLevel
{
    public class UpdateUserProfileModernLanguageLevelCommandValidator : AbstractValidator<UpdateUserProfileModernLanguageLevelCommand>
    {
        public UpdateUserProfileModernLanguageLevelCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x=> x.Data.Id)
                .SetValidator(new ItemMustExistValidator<ModernLanguageLevel>(appDbContext, ValidationCodes.MODERN_LANGUAGE_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data)
                .SetValidator(new ModernLanguageLevelValidator(appDbContext));
        }
    }
}
