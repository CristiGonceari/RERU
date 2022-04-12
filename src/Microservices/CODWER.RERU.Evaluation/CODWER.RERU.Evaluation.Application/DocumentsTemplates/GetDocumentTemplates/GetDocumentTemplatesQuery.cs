using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.GetDocumentTemplates
{
    public class GetDocumentTemplatesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<AddEditDocumentTemplateDto>>
    {
        public string Name { get; set; }
    }
}
