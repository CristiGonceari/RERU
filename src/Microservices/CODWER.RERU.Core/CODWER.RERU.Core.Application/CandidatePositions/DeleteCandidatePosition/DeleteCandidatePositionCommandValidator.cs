using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Core.Application.CandidatePositions.DeleteCandidatePosition
{
    public class DeleteCandidatePositionCommandValidator : AbstractValidator<DeleteCandidatePositionCommand>
    {
        public DeleteCandidatePositionCommandValidator(CoreDbContext coreDbContext)
        {
            RuleFor(x => x.Id)
               .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(coreDbContext, ValidationCodes.INVALID_POSITION,
                   ValidationMessages.NotFound));
        }
    }
}
