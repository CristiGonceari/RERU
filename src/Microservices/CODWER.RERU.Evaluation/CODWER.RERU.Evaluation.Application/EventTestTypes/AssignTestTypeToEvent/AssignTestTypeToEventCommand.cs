using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes.AssignTestTypeToEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENT_TEST_TYPES_GENERAL_ACCESS)]
    public class AssignTestTypeToEventCommand : IRequest<Unit>
    {
        public AddEventTestTypeDto Data { get; set; }
    }
}
