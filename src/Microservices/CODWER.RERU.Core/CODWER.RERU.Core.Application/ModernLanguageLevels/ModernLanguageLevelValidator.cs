using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels
{
    public class ModernLanguageLevelValidator : AbstractValidator<AddEditModernLanguageLevelDto> 
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly AppDbContext _appDbContext;

        public ModernLanguageLevelValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _appDbContext = appDbContext;

            RuleFor(x => x.ModernLanguageId)
                .SetValidator(new ItemMustExistValidator<ModernLanguage>(appDbContext, ValidationCodes.MODERN_LANGUAGE_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => (int)x.KnowledgeQuelifiers)
                .SetValidator(new ExistInEnumValidator<KnowledgeQuelifiersEnum>());

            RuleFor(x => x)
              .Must(x => CheckIfCurrentUser(x.ContractorId).Result)
              .WithErrorCode(ValidationCodes.USER_NOT_FOUND);
        }
        private async Task<bool> CheckIfCurrentUser(int id)
        {
            var currentUser = await _currentUserProvider.Get();
            var contractor = _appDbContext.Contractors.Include(up => up.UserProfile).FirstOrDefault(up => up.UserProfileId.ToString() == currentUser.Id);

            if (contractor.Id == id)
            {
                return true;
            }

            return false;
        }
    }
}
