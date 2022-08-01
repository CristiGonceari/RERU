﻿using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels.BulkAddEditModernLanguageLevels
{
    public class BulkAddEditModernLanguageLevelsCommandValidator : AbstractValidator<BulkAddEditModernLanguageLevelsCommand>
    {
        public BulkAddEditModernLanguageLevelsCommandValidator(AppDbContext appDbContext)
        {
            RuleForEach(x => x.Data)
                .SetValidator(new ModernLanguageLevelValidator(appDbContext));
        }
    }
}
