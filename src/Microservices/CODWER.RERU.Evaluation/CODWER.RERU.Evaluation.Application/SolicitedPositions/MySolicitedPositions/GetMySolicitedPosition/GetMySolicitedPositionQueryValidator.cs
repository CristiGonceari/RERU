using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.GetMySolicitedPosition
{
    public class GetMySolicitedPositionQueryValidator : AbstractValidator<GetMySolicitedPositionQuery>
    {
        public GetMySolicitedPositionQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<SolicitedVacantPosition>(appDbContext, ValidationCodes.INVALID_SOLICITED_POSITION,
                    ValidationMessages.InvalidReference));
        }
    }
}
