using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.GetEventTestTemplates
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]
    public class GetEventTestTemplatesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestTemplateDto>>
    {
        public int EventId { get; set; }
    }
}
