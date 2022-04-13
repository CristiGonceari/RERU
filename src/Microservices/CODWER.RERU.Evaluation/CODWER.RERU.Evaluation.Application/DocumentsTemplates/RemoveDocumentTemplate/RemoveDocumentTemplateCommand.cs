using MediatR;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.RemoveDocumentTemplate
{
    public class RemoveDocumentTemplateCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
