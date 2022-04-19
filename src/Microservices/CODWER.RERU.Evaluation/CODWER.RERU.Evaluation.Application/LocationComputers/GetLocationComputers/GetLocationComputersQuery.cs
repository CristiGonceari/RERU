using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.LocationComputers.GetLocationComputers
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_LOCAȚII)]
    public class GetLocationComputersQuery : PaginatedQueryParameter, IRequest<PaginatedModel<LocationClientDto>>
    {
        public int LocationId { get; set; }
    }
}
