using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels.UpdateContractorModernLanguageLevel
{
    public class UpdateContractorModernLanguageLevelCommandValidator : AbstractValidator<UpdateContractorModernLanguageLevelCommand>
    {
        public UpdateContractorModernLanguageLevelCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<ModernLanguageLevel>(appDbContext, ValidationCodes.MODERN_LANGUAGE_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data)
                .SetValidator(new ModernLanguageLevelValidator(appDbContext));
        }
    }
}
