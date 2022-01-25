using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Penalizations.GetPenalization
{
    public class GetPenalizationQueryValidator : AbstractValidator<GetPenalizationQuery>
    {
        public GetPenalizationQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Penalization>(appDbContext,ValidationCodes.PENALIZATION_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
