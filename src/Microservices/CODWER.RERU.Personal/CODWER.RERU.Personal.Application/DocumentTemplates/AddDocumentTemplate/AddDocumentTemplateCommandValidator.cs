using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.AddDocumentTemplate
{
    public class AddDocumentTemplateCommandValidator : AbstractValidator<AddDocumentTemplateCommand>
    {
        public AddDocumentTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new DocumentTemplateValidator());
        }
    }
}
