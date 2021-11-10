using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.DeleteLocation
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATIONS_GENERAL_ACCESS)]
    public class DeleteLocationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
