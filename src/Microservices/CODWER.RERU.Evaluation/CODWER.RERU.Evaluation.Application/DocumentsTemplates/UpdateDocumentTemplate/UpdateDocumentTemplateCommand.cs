using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.UpdateDocumentTemplate
{
    public class UpdateDocumentTemplateCommand : IRequest<Unit>
    {
        public AddEditDocumentTemplateDto Data { get; set; }
    }
}
