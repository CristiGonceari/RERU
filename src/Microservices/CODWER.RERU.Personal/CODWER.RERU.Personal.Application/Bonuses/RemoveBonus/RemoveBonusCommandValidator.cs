using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Bonuses.RemoveBonus
{
    public class RemoveBonusCommandValidator : AbstractValidator<RemoveBonusCommand>
    {
        public RemoveBonusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Bonus>(appDbContext, ValidationCodes.BONUS_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
