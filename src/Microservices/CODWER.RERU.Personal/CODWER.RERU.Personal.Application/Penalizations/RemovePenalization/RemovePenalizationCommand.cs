using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Penalizations.RemovePenalization
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_PENALIZARI)]

    public class RemovePenalizationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
