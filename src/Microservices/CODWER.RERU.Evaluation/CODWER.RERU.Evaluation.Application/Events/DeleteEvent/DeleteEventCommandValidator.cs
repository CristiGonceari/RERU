using FluentValidation;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Events.DeleteEvent
{
    public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteEventCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Id)
                .MustAsync((id, cancellation) => EventInUse(id))
                .WithErrorCode(ValidationCodes.CAN_NOT_DELETE_EVENT_IN_USE)
                .WithMessage(ValidationMessages.CannotDeleteEventInUse);
        }

        private async Task<bool> EventInUse(int id)
        {
            var eventToDelete = _appDbContext.Events.Find(id);

            if (eventToDelete.TillDate > System.DateTime.Now) 
                return false;

            return true;
        }
    }
}
