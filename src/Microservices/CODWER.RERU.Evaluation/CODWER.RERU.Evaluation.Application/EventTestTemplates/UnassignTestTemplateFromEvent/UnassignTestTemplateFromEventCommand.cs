using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.UnassignTestTypeFromEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENTS_GENERAL_ACCESS)]
    public class UnassignTestTemplateFromEventCommand : IRequest<Unit>
    {
        public int EventId { get; set; }
        public int TestTypeId { get; set; }
    }
}
