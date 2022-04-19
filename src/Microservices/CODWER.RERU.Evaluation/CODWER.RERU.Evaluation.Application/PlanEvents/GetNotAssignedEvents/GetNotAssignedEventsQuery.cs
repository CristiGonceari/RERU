using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.GetNotAssignedEvents
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_PLANURI)]
    public class GetNotAssignedEventsQuery : IRequest<List<EventDto>>
    {
        public string Keyword { get; set; }
        public int PlanId { get; set; }
    }
}
