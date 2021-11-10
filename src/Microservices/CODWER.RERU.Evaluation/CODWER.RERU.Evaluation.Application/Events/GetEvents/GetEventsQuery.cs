using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Events.GetEvents
{
    [ModuleOperation(permission: PermissionCodes.EVENTS_GENERAL_ACCESS)]
    public class GetEventsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<EventDto>>
    {
        public string Name { get; set; }
        public string LocationKeyword { get; set; }
    }
}
