using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Attestations.RemoveAttestation
{
    [ModuleOperation(permission: PermissionCodes.ATTESTATIONS_GENERAL_ACCESS)]

    public class RemoveAttestationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
