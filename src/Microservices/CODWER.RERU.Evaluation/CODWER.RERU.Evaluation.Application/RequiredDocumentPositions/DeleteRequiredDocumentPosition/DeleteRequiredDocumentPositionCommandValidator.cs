using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.DeleteRequiredDocumentPosition
{
    public class DeleteRequiredDocumentPositionCommandValidator : AbstractValidator<DeleteRequiredDocumentPositionCommand>
    {
        public DeleteRequiredDocumentPositionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<RequiredDocumentPosition>(appDbContext, ValidationCodes.INVALID_REQUIRED_DOCUMENT, ValidationMessages.NotFound));
        }
    }
}
