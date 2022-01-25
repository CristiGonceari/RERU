using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.GetDocumentTemplates
{
    [ModuleOperation(permission: PermissionCodes.DOCUMENTS_TEMPLATE_GENERAL_ACCESS)]
    public class GetDocumentTemplatesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<AddEditDocumentTemplateDto>>
    {
        public string Name { get; set; }
    }
}
