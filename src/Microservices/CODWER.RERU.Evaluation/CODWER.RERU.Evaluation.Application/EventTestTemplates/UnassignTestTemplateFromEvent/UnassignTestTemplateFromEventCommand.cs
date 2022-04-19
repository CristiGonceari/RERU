using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.UnassignTestTemplateFromEvent
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]
    public class UnassignTestTemplateFromEventCommand : IRequest<Unit>
    {
        public int EventId { get; set; }
        public int TestTemplateId { get; set; }
    }
}
