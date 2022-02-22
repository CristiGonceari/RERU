using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.AssignTestTemplateToEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENTS_GENERAL_ACCESS)]
    public class AssignTestTemplateToEventCommand : IRequest<Unit>
    {
        public AddEventTestTemplateDto Data { get; set; }
    }
}
