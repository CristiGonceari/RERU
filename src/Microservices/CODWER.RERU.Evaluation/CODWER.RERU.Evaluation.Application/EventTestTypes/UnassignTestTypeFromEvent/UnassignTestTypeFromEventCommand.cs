using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes.UnassignTestTypeFromEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENT_TEST_TYPES_GENERAL_ACCESS)]
    public class UnassignTestTypeFromEventCommand : IRequest<Unit>
    {
        public int EventId { get; set; }
        public int TestTypeId { get; set; }
    }
}
