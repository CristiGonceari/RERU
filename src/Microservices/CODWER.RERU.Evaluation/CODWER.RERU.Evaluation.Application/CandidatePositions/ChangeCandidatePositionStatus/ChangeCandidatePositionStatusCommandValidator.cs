using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.ChangeCandidatePositionStatus
{
    public class ChangeCandidatePositionStatusCommandValidator : AbstractValidator<ChangeCandidatePositionStatusCommand>
    {
        public ChangeCandidatePositionStatusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.PositionId)
                .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(appDbContext, ValidationCodes.INVALID_POSITION,
                    ValidationMessages.NotFound));
        }
    }
}
