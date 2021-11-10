using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.AssignResponsiblePersonToLocation
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATION_RESPONSIBLE_PERSONS_GENERAL_ACCESS)]
    public class AssignResponsiblePersonToLocationCommand : IRequest<Unit>
    {
        public AddLocationPersonDto Data { get; set; }
    }
}
