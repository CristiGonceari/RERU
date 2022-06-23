using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions.GetEventVacantPosition
{
    public class GetEventVacantPositionQueryValidator : AbstractValidator<GetEventVacantPositionQuery>
    {
        public GetEventVacantPositionQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(
                    new ItemMustExistValidator<EventVacantPosition>(appDbContext, ValidationCodes.INVALID_ID, ValidationMessages.NotFound)
                );
        }
    }
}
