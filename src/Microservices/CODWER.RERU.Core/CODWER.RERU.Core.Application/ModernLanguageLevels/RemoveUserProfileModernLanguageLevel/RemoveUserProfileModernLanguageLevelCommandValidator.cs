using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.RemoveUserProfileModernLanguageLevel
{
    public class RemoveUserProfileModernLanguageLevelCommandValidator : AbstractValidator<RemoveUserProfileModernLanguageLevelCommand>
    {
        public RemoveUserProfileModernLanguageLevelCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.ModernLanguageLevelId)
                .SetValidator(new ItemMustExistValidator<ModernLanguageLevel>(appDbContext, ValidationCodes.MODERN_LANGUAGE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
