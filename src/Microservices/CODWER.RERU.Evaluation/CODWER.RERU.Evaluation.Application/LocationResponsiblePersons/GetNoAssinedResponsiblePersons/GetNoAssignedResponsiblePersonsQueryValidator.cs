using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities;

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
