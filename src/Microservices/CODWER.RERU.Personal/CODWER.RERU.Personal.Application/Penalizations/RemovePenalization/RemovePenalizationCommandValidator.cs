using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Penalizations.RemovePenalization
{
    public class RemovePenalizationCommandValidator : AbstractValidator<RemovePenalizationCommand>
    {
        public RemovePenalizationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Penalization>(appDbContext, ValidationCodes.PENALIZATION_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
