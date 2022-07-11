using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.DeleteRequiredDocument
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_DOCUMENTE)]
    public class DeleteRequiredDocumentsCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
