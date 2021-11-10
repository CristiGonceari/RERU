using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.AssignLocationComputer
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATION_CLIENTS_GENERAL_ACCESS)]
    public class AssignLocationComputerCommand : IRequest<string>
    {
        public AddLocationClientDto Data { get; set; }

    }
}
