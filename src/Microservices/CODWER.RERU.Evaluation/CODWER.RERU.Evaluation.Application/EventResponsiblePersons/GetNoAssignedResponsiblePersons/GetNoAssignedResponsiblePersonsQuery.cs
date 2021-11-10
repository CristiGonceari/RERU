using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetNoAssignedResponsiblePersons
{
    [ModuleOperation(permission: PermissionCodes.EVENT_RESPONSIBLE_PERSONS_GENERAL_ACCESS)]
    public class GetNoAssignedResponsiblePersonsQuery : IRequest<List<UserProfileDto>>
    {
        public int EventId { get; set; }
        public string Keyword { get; set; }
    }
}
