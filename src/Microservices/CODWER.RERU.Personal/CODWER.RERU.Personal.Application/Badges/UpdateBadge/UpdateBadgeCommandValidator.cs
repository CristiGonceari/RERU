using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Badges.UpdateBadge
{
    public class UpdateBadgeCommandValidator : AbstractValidator<UpdateBadgeCommand>
    {
        public UpdateBadgeCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Badge>(appDbContext, ValidationCodes.BADGE_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new BadgeValidator(appDbContext));
        }
    }
}
