using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.GetLocations
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_LOCAȚII)]
    public class GetLocationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<LocationDto>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
