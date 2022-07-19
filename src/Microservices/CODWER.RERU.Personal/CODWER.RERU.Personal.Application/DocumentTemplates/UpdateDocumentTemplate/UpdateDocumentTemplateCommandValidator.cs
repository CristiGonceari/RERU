using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities.Documents;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.UpdateDocumentTemplate
{
    public class UpdateDocumentTemplateCommandValidator : AbstractValidator<UpdateDocumentTemplateCommand>
    {
        public UpdateDocumentTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<HrDocumentTemplate>(appDbContext, ValidationCodes.DOCUMENTS_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new DocumentTemplateValidator());
        }
    }
}
