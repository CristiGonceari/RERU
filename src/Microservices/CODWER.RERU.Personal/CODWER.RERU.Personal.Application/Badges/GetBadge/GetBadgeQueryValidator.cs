using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Badges.GetBadge
{
    public class GetBadgeQueryValidator : AbstractValidator<GetBadgeQuery>
    {
        public GetBadgeQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Badge>(appDbContext,ValidationCodes.BADGE_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
