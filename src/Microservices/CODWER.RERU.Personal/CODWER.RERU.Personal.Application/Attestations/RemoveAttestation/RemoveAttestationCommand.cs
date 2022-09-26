using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Attestations.RemoveAttestation
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ATESTARI)]

    public class RemoveAttestationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
