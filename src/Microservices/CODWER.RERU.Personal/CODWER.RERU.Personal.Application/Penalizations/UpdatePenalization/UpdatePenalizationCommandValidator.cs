using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Penalizations.UpdatePenalization
{
    public class UpdatePenalizationCommandValidator : AbstractValidator<UpdatePenalizationCommand>
    {
        public UpdatePenalizationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Penalization>(appDbContext, ValidationCodes.PENALIZATION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new PenalizationValidator(appDbContext));
        }
    }
}
