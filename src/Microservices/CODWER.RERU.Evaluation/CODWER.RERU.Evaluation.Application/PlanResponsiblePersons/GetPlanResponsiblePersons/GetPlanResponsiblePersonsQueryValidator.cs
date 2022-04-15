using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.GetPlanResponsiblePersons
{
    public class GetPlanResponsiblePersonsQueryValidator : AbstractValidator<GetPlanResponsiblePersonsQuery>
    {
        public GetPlanResponsiblePersonsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.PlanId)
                .SetValidator(x => new ItemMustExistValidator<Plan>(appDbContext, ValidationCodes.INVALID_PLAN,
                    ValidationMessages.InvalidReference));
        }
    }
}
