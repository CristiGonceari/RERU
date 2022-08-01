using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.MilitaryObligations.RemoveMilitaryObligation
{
    public class RemoveMilitaryObligationCommandValidator : AbstractValidator<RemoveMilitaryObligationCommand>
    {
        public RemoveMilitaryObligationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<MilitaryObligation>(appDbContext, ValidationCodes.MILITARY_OBLIGATION_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
