using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Events.AddEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENTS_GENERAL_ACCESS)]
    public class AddEventCommand : IRequest<int>
    {
        public AddEditEventDto Data { get; set; }
    }
}
