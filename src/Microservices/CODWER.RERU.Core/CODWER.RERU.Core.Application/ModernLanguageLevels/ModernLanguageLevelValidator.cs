using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels
{
    public class ModernLanguageLevelValidator : AbstractValidator<AddEditModernLanguageLevelDto> 
    {
        public ModernLanguageLevelValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.ModernLanguageId)
                .SetValidator(new ItemMustExistValidator<ModernLanguage>(appDbContext, ValidationCodes.MODERN_LANGUAGE_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => (int)x.KnowledgeQuelifiers)
                .SetValidator(new ExistInEnumValidator<KnowledgeQuelifiersEnum>());
        }
    }
}
