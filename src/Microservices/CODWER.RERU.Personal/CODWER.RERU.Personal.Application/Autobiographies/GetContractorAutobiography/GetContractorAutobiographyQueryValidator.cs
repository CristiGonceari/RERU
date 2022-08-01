using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.Autobiographies.GetContractorAutobiography
{
    public class GetContractorAutobiographyQueryValidator : AbstractValidator<GetContractorAutobiographyQuery>
    {
        public GetContractorAutobiographyQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                   ValidationMessages.InvalidReference));
        }
    }
}
