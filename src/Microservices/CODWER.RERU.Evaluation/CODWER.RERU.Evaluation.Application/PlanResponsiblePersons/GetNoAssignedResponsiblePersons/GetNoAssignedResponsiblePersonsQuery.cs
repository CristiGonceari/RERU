using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.GetNoAssignedResponsiblePersons
{
    [ModuleOperation(permission: PermissionCodes.PLANS_GENERAL_ACCESS)]
    public class GetNoAssignedResponsiblePersonsQuery : IRequest<List<UserProfileDto>>
    {
        public int PlanId { get; set; }
        public string Keyword { get; set; }
    }
}
