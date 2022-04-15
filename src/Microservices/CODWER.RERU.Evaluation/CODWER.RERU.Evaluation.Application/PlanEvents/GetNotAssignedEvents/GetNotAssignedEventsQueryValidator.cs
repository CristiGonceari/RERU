using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.GetNotAssignedEvents
{
    public class GetPlanEventsQueryValidator : AbstractValidator<GetNotAssignedEventsQuery>
    {
        public GetPlanEventsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.PlanId)
                .SetValidator(x => new ItemMustExistValidator<Plan>(appDbContext, ValidationCodes.INVALID_PLAN,
                    ValidationMessages.InvalidReference));
        }
    }
}
