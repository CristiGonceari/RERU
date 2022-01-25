using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Penalizations.RemovePenalization
{
    [ModuleOperation(permission: PermissionCodes.PENALIZATIONS_GENERAL_ACCESS)]

    public class RemovePenalizationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
