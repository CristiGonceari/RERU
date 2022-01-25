using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.GetDocumentTemplate
{
    [ModuleOperation(permission: PermissionCodes.DOCUMENTS_TEMPLATE_GENERAL_ACCESS)]
    public class GetDocumentTemplateQuery : IRequest<AddEditDocumentTemplateDto>
    {
        public int Id { get; set; }
    }
}
