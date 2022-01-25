using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.RemoveDocumentTemplate
{
    [ModuleOperation(permission: PermissionCodes.DOCUMENTS_TEMPLATE_GENERAL_ACCESS)]
    public class RemoveDocumentTemplateCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
