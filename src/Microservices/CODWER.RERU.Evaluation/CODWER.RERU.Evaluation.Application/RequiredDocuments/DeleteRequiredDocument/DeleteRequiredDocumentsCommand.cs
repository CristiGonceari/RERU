using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.DeleteRequiredDocument
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_DOCUMENTE_NECESARE)]
    public class DeleteRequiredDocumentsCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
