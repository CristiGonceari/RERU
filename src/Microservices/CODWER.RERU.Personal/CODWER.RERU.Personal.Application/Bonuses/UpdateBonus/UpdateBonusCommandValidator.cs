using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Bonuses.UpdateBonus
{
    public class UpdateBonusCommandValidator : AbstractValidator<UpdateBonusCommand>
    {
        public UpdateBonusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Bonus>(appDbContext, ValidationCodes.BONUS_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new BonusValidator(appDbContext));
        }
    }
}
