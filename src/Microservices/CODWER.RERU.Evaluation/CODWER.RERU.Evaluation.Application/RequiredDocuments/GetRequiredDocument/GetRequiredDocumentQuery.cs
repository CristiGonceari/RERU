using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.GetRequiredDocument
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_DOCUMENTE)]
    public class GetRequiredDocumentQuery : IRequest<RequiredDocumentDto>
    {
        public int Id { get; set; }
    }
}
