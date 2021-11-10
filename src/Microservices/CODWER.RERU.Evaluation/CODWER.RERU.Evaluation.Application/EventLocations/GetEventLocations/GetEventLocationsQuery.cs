using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventLocations.GetEventLocations
{
    [ModuleOperation(permission: PermissionCodes.EVENT_LOCATIONS_GENERAL_ACCESS)]
    public class GetEventLocationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<LocationDto>>
    {
        public int EventId { get; set; }
    }
}
