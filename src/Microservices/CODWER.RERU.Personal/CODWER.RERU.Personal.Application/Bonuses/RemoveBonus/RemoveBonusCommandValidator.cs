using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
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
