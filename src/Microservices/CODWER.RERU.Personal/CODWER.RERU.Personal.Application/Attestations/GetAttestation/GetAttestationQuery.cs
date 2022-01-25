using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Attestations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Attestations.GetAttestation
{
    [ModuleOperation(permission: PermissionCodes.ATTESTATIONS_GENERAL_ACCESS)]

    public class GetAttestationQuery : IRequest<AttestationDto>
    {
        public int Id { get; set; }
    }
}
