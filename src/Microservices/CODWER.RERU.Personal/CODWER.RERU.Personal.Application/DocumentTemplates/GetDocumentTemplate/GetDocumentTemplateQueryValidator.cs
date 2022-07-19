using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.Documents;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.GetDocumentTemplate
{
    public class GetDocumentTemplateQueryValidator : AbstractValidator<GetDocumentTemplateQuery>
    {
        public GetDocumentTemplateQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<DocumentTemplate>(appDbContext, ValidationCodes.DOCUMENTS_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
