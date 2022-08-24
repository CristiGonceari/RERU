using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.DeleteCandidatePosition
{
    public class DeleteCandidatePositionCommandValidator : AbstractValidator<DeleteCandidatePositionCommand>
    {
        public DeleteCandidatePositionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
               .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(appDbContext, ValidationCodes.INVALID_POSITION,
                   ValidationMessages.NotFound));

            RuleFor(x => x.Id)
                .Must(x => !appDbContext.SolicitedVacantPositions.Any(s => s.CandidatePositionId.Value == x))
                .WithErrorCode(ValidationCodes.CAN_NOT_DELETE_USED_CANDIDATE_POSITION);
        }
    }
}
