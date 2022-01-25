using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.Documents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.RemoveDocumentTemplate
{
   public class RemoveDocumentTemplateCommandValidator : AbstractValidator<RemoveDocumentTemplateCommand>
    {
        public RemoveDocumentTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<DocumentTemplate>(appDbContext, ValidationCodes.DOCUMENTS_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
