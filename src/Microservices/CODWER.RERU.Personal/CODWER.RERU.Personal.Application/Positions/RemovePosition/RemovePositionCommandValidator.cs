using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Positions.RemovePosition
{
    public class RemovePositionCommandValidator : AbstractValidator<RemovePositionCommand>
    {
        public RemovePositionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Position>(appDbContext,ValidationCodes.POSITION_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
