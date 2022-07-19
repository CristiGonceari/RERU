using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities.Documents;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.RemoveDocumentTemplate
{
   public class RemoveDocumentTemplateCommandValidator : AbstractValidator<RemoveDocumentTemplateCommand>
    {
        public RemoveDocumentTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<HrDocumentTemplate>(appDbContext, ValidationCodes.DOCUMENTS_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
