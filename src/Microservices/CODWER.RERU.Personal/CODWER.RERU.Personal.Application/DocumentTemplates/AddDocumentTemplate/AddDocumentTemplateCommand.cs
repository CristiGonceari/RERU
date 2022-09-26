using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.AddDocumentTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_SABLOANE_DE_DOCUMENTE)]
    public class AddDocumentTemplateCommand : IRequest<int>
    {
        public AddEditDocumentTemplateDto Data { get; set; }
    }
}
