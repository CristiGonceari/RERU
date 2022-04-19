using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.AddLocation
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_LOCAȚII)]
    public class AddLocationCommand : IRequest<int>
    {
        public AddEditLocationDto Data { get; set; }
    }
}
