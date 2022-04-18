using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.GetEvent
{
    public class GetEventQueryValidator : AbstractValidator<GetEventQuery>
    {
        public GetEventQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Event>(appDbContext, ValidationCodes.INVALID_EVENT,
                    ValidationMessages.InvalidReference));
        }
    }
}
