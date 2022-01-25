using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Bonuses.GetBonus
{
    public class GetBonusQueryValidator : AbstractValidator<GetBonusQuery>
    {
        public GetBonusQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Bonus>(appDbContext,ValidationCodes.BONUS_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
