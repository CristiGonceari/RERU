using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.AssignResponsiblePersonToEvent
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]
    public class AssignResponsiblePersonToEventCommand : IRequest<List<int>>
    {
        public int EventId { get; set; }
        public List<int> UserProfileId { get; set; }
    }
}
