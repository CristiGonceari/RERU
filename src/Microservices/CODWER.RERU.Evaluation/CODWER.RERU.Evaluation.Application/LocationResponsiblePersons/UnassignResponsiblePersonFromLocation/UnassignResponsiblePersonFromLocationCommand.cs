using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.UnassignResponsiblePersonFromLocation
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_LOCAȚII)]
    public class UnassignResponsiblePersonFromLocationCommand : IRequest<Unit>
    {
        public int LocationId { get; set; }
        public int UserProfileId { get; set; }
    }
}
