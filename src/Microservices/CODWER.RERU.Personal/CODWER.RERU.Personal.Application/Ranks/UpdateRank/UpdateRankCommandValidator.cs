using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Ranks.UpdateRank
{
    public class UpdateRankCommandValidator : AbstractValidator<UpdateRankCommand>
    {
        public UpdateRankCommandValidator(AppDbContext appDbContext, IDateTime dateTime)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Rank>(appDbContext,ValidationCodes.RANK_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new RankValidator(appDbContext, dateTime));
        }
    }
}
