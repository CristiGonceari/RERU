using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.DeleteRequiredDocument
{
    public class DeleteRequiredDocumentsCommandValidator : AbstractValidator<DeleteRequiredDocumentsCommand>
    {
        public DeleteRequiredDocumentsCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<RequiredDocument>(appDbContext, ValidationCodes.INVALID_REQUIRED_DOCUMENT, ValidationMessages.NotFound));
        }
    }
}
