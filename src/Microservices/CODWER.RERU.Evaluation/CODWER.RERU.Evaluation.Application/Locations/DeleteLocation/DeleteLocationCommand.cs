using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.DeleteLocation
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_LOCATII)]
    public class DeleteLocationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
