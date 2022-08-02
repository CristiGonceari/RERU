using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using CODWER.RERU.Personal.DataTransferObjects.ModernLanguageLevel;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels
{
    public class ModernLanguageLevelValidator : AbstractValidator<AddEditModernLanguageLevelDto>
    {
        public ModernLanguageLevelValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.ModernLanguageId)
                .SetValidator(new ItemMustExistValidator<ModernLanguage>(appDbContext, ValidationCodes.MODERN_LANGUAGE_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_DEPARTMENT_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => (int)x.KnowledgeQuelifiers)
                .SetValidator(new ExistInEnumValidator<KnowledgeQuelifiersEnum>());
        }
    }
}
