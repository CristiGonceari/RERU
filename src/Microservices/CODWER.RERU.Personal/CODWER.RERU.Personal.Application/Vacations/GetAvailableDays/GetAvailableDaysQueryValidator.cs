using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Vacations.GetAvailableDays
{
    public class GetAvailableDaysQueryValidator : AbstractValidator<GetAvailableDaysQuery>
    {
        public GetAvailableDaysQueryValidator(AppDbContext appDbContext)
        {
             RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
