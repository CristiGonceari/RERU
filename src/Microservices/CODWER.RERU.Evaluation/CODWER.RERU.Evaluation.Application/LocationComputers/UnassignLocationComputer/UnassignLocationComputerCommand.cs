using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.LocationComputers.UnassignLocationComputer
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATIONS_GENERAL_ACCESS)]
    public class UnassignLocationComputerCommand : IRequest<Unit>
    {
        public int LocationClientId { get; set; }
    }
}
