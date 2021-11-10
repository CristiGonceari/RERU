using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.GetLocationComputers
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATION_CLIENTS_GENERAL_ACCESS)]
    public class GetLocationComputersQuery : PaginatedQueryParameter, IRequest<PaginatedModel<LocationClientDto>>
    {
        public int LocationId { get; set; }
    }
}
