using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.EditLocation
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATIONS_GENERAL_ACCESS)]
    public class EditLocationCommand : IRequest<int>
    {
        public AddEditLocationDto Data { get; set; }
    }
}
