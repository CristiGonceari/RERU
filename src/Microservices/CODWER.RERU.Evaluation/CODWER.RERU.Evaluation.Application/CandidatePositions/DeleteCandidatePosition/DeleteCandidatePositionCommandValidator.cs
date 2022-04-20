using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.DeleteCandidatePosition
{
    public class DeleteCandidatePositionCommandValidator : AbstractValidator<DeleteCandidatePositionCommand>
    {
        public DeleteCandidatePositionCommandValidator(AppDbContext coreDbContext)
        {
            RuleFor(x => x.Id)
               .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(coreDbContext, ValidationCodes.INVALID_POSITION,
                   ValidationMessages.NotFound));
        }
    }
}
