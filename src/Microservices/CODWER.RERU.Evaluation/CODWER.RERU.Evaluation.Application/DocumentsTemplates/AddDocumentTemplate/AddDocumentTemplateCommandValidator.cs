using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.AddDocumentTemplate
{
    class AddDocumentTemplateCommandValidator : AbstractValidator<AddDocumentTemplateCommand>
    {
        public AddDocumentTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new DocumentTemplateValidator());
        }
    }
}
