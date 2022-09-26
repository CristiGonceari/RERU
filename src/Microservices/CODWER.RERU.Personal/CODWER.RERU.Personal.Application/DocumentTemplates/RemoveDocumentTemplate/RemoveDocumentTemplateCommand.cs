using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.RemoveDocumentTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_SABLOANE_DE_DOCUMENTE)]
    public class RemoveDocumentTemplateCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
