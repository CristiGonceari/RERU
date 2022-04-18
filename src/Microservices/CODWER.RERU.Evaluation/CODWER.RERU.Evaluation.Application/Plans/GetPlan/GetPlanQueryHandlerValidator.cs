using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlan
{
    public class GetPlanQueryHandlerValidator : AbstractValidator<GetPlanQuery>
    {
        public GetPlanQueryHandlerValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(
                    new ItemMustExistValidator<Plan>(appDbContext, ValidationCodes.INVALID_ID, ValidationMessages.NotFound)
                    );
        }
    }
}
