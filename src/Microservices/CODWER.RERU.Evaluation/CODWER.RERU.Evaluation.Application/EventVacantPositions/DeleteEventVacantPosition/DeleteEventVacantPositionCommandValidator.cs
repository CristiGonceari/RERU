using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions.DeleteEventVacantPosition
{
    public class DeleteEventVacantPositionCommandValidator : AbstractValidator<DeleteEventVacantPositionCommand>
    {
        public DeleteEventVacantPositionCommandValidator(AppDbContext appDbContext)
        {

            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<EventVacantPosition>(appDbContext, ValidationCodes.INVALID_EVENT, ValidationMessages.NotFound));
        }
    }
}
