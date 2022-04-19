using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.UnassignEvaluatorFromEvent
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]
    public class UnassignEvaluatorFromEventCommand : IRequest<Unit>
    {
        public int EventId { get; set; }
        public int EvaluatorId { get; set; }
    }
}
