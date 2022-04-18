using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.Documents;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.UpdateDocumentTemplate
{
    public class UpdateDocumentTemplateCommandValidator : AbstractValidator<UpdateDocumentTemplateCommand>
    {
        public UpdateDocumentTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<DocumentTemplate>(appDbContext, ValidationCodes.DOCUMENTS_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new DocumentTemplateValidator());
        }
    }
}
