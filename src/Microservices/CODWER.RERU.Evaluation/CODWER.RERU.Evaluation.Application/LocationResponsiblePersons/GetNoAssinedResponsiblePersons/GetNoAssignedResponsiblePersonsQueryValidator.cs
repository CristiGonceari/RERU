using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.GetNoAssinedResponsiblePersons
{
    public class GetNoAssignedResponsiblePersonsQueryValidator : AbstractValidator<GetNoAssignedResponsiblePersonsQuery>
    {
        public GetNoAssignedResponsiblePersonsQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.LocationId)
                .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_LOCATION,
                    ValidationMessages.InvalidReference));
        }
    }
}
