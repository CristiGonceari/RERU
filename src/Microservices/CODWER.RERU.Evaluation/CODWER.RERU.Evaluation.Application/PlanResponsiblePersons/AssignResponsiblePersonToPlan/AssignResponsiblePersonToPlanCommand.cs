using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.AssignResponsiblePersonToPlan
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_PLANURI)]

    public class AssignResponsiblePersonToPlanCommand : IRequest<List<int>>
    {
        public int PlanId { get; set; }
        public List<int> UserProfileId { get; set; }
    }
}
