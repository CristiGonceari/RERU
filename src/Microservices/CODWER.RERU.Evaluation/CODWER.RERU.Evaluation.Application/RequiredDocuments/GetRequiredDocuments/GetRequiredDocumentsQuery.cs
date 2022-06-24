using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.GetRequiredDocuments
{
    public class GetRequiredDocumentsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<RequiredDocumentDto>>
    {
        public string Name { get; set; }
        public bool? Mandatory { get; set; }
    }
}
