using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

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
