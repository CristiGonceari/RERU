using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.MilitaryObligations.UpdateMilitaryObligation
{
    public class UpdateMilitaryObligationCommandValidator : AbstractValidator<UpdateMilitaryObligationCommand>
    {
        public UpdateMilitaryObligationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                           .SetValidator(new ItemMustExistValidator<MilitaryObligation>(appDbContext, ValidationCodes.MILITARY_OBLIGATION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data)
                .SetValidator(new MilitaryObligationValidator(appDbContext));
        }
    }
}
