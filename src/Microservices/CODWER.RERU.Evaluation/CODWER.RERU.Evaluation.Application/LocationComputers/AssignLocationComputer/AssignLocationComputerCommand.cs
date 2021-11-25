using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.LocationComputers.AssignLocationComputer
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATIONS_GENERAL_ACCESS)]
    public class AssignLocationComputerCommand : IRequest<string>
    {
        public AddLocationClientDto Data { get; set; }

    }
}
