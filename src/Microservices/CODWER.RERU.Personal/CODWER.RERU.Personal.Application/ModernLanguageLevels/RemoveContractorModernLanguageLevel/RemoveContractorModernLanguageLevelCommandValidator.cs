using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels.RemoveContractorModernLanguageLevel
{
    public class RemoveContractorModernLanguageLevelCommandValidator : AbstractValidator<RemoveContractorModernLanguageLevelCommand>
    {
        public RemoveContractorModernLanguageLevelCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.ModernLanguageLevelId)
                .SetValidator(new ItemMustExistValidator<ModernLanguageLevel>(appDbContext, ValidationCodes.MODERN_LANGUAGE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
