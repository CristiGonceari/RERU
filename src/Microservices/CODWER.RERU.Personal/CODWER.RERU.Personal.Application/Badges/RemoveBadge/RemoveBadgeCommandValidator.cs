using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Badges.RemoveBadge
{
    public class RemoveBadgeCommandValidator : AbstractValidator<RemoveBadgeCommand>
    {
        public RemoveBadgeCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Badge>(appDbContext, ValidationCodes.BADGE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
