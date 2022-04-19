using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.LocationComputers.UnassignLocationComputer
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_LOCAȚII)]
    public class UnassignLocationComputerCommand : IRequest<Unit>
    {
        public int LocationClientId { get; set; }
    }
}
