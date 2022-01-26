using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Ranks.RemoveRank
{
    public class RemoveRankCommandValidator : AbstractValidator<RemoveRankCommand>
    {
        public RemoveRankCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Rank>(appDbContext,ValidationCodes.RANK_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
