using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Events.GetEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENTS_GENERAL_ACCESS)]
    public class GetEventQuery : IRequest<EventDto>
    {
        public int Id { get; set; }
    }
}
