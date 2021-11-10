using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.UnassignResponsiblePersonFromLocation
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATION_RESPONSIBLE_PERSONS_GENERAL_ACCESS)]
    public class UnassignResponsiblePersonFromLocationCommand : IRequest<Unit>
    {
        public AddLocationPersonDto Data { get; set; }
    }
}
