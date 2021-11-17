using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

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
