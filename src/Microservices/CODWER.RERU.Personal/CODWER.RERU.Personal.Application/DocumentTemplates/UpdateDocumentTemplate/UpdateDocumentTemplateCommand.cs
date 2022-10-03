using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.UpdateDocumentTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_SABLOANE_DE_DOCUMENTE)]
    public class UpdateDocumentTemplateCommand : IRequest<Unit>
    {
        public AddEditDocumentTemplateDto Data { get; set; }
    }
}
