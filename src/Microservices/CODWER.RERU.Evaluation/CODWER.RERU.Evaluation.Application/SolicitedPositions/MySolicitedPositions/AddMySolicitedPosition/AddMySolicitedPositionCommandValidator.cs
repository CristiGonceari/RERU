using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.AddMySolicitedPosition
{
    public class AddMySolicitedPositionCommandValidator : AbstractValidator<AddMySolicitedPositionCommand>
    {
        public AddMySolicitedPositionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.CandidatePositionId)
                .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(appDbContext, ValidationCodes.INVALID_POSITION,
                    ValidationMessages.InvalidReference));
        }
    }
}
