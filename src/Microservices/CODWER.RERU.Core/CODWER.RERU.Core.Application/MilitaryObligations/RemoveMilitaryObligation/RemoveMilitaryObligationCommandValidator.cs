using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.MilitaryObligations.RemoveMilitaryObligation
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
