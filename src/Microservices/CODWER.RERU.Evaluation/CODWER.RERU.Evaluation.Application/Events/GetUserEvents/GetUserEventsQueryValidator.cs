using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.Events.GetUserEvents
{
    public class GetUserEventsQueryValidator : AbstractValidator<GetUserEventsQuery>
    {
        public GetUserEventsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.UserId)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                    ValidationMessages.InvalidReference));
        }
    }
}
