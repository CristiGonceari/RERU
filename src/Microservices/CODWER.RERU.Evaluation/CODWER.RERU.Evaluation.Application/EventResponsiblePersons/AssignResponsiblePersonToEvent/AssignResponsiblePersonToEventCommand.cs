using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.AssignResponsiblePersonToEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENT_RESPONSIBLE_PERSONS_GENERAL_ACCESS)]
    public class AssignResponsiblePersonToEventCommand : IRequest<Unit>
    {
        public AddEventPersonDto Data { get; set; }
    }
}
