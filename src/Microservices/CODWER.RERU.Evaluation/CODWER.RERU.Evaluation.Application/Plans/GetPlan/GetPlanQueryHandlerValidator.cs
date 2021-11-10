using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

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
