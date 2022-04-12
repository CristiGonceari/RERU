using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.AddDocumentTemplate
{
    public class AddDocumentTemplateCommand : IRequest<int>
    {
        public AddEditDocumentTemplateDto Data { get; set; }
    }
}
