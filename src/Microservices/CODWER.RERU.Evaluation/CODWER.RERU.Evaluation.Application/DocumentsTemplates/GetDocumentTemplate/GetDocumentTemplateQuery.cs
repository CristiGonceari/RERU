using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.GetDocumentTemplate
{
    public class GetDocumentTemplateQuery : IRequest<AddEditDocumentTemplateDto>
    {
        public int Id { get; set; }
    }
}
