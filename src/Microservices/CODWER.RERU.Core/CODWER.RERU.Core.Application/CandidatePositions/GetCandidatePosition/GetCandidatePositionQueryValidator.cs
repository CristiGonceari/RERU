using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.CandidatePositions.GetCandidatePosition
{
    public class GetCandidatePositionQueryValidator : AbstractValidator<GetCandidatePositionQuery>
    {
        public GetCandidatePositionQueryValidator(AppDbContext coreDbContext)
        {
            RuleFor(x => x.Id)
               .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(coreDbContext, ValidationCodes.INVALID_POSITION,
                   ValidationMessages.NotFound));
        }
    }
}
