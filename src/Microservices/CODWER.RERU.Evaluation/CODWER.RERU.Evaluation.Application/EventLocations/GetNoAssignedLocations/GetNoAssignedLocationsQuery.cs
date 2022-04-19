using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventLocations.GetNoAssignedLocations
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]
    public class GetNoAssignedLocationsQuery : IRequest<List<LocationDto>>
    {
        public int EventId { get; set; }
        public string Keyword { get; set; }
    }
}
