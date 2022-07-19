using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contractors.UpdateContractor
{
    public class UpdateContractorCommandValidator : AbstractValidator<UpdateContractorCommand>
    {
        public UpdateContractorCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new ContractorValidator(appDbContext));
        }
    }
}
