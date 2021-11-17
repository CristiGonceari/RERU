using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.GetNoAssinedResponsiblePersons
{
    [ModuleOperation(permission: PermissionCodes.LOCATION_RESPONSIBLE_PERSONS_GENERAL_ACCESS)]
    public class GetNoAssignedResponsiblePersonsQuery : IRequest<List<UserProfileDto>>
    {
        public string Keyword { get; set; }
        public int LocationId { get; set; }
    }
}
