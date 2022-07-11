using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.GetRequiredDocuments
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_DOCUMENTE)]
    public class GetRequiredDocumentsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<RequiredDocumentDto>>
    {
        public string Name { get; set; }
        public bool? Mandatory { get; set; }
    }
}
