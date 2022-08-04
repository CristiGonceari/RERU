using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Vacations.RemoveVacation
{
    public class RemoveVacationCommandValidator : AbstractValidator<RemoveVacationCommand>
    {
        public RemoveVacationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Vacation>(appDbContext, ValidationCodes.VACATION_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
