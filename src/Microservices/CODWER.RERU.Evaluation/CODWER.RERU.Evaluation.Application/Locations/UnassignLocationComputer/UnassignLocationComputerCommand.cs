using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.UnassignLocationComputer
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATION_CLIENTS_GENERAL_ACCESS)]
    public class UnassignLocationComputerCommand : IRequest<Unit>
    {
        public int LocationClientId { get; set; }
    }
}
