using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.AssignEvaluatorToEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENT_EVALUATORS_GENERAL_ACCESS)]
    public class AssignEvaluatorToEventCommand : IRequest<Unit>
    {
        public AddEventEvaluatorDto Data { get; set; }
    }
}
