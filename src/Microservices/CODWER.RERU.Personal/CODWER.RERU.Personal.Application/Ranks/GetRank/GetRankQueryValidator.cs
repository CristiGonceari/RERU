using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Ranks.GetRank
{
    public class GetRankQueryValidator : AbstractValidator<GetRankQuery>
    {
        public GetRankQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Rank>(appDbContext,ValidationCodes.RANK_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
