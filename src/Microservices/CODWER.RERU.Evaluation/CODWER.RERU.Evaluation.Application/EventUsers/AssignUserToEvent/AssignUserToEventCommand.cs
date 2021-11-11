using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventUsers.AssignUserToEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENT_USERS_GENERAL_ACCESS)]
    public class AssignUserToEventCommand : IRequest<Unit>
    {
        public AddEventPersonDto Data { get; set; }
    }
}
