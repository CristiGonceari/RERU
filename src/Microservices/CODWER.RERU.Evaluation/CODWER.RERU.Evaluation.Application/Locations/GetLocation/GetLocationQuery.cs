using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.GetLocation
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_LOCATII)]
    public class GetLocationQuery : IRequest<LocationDto>
    {
        public int Id { get; set; }
    }
}
