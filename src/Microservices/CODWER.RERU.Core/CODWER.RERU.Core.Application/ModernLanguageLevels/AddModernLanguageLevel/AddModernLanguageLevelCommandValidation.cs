﻿using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.AddModernLanguageLevel
{
    public class AddModernLanguageLevelCommandValidation : AbstractValidator<AddModernLanguageLevelCommand>
    {
        public AddModernLanguageLevelCommandValidation(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new ModernLanguageLevelValidator(appDbContext));
        }
    }
}
