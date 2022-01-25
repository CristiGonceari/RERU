using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Attestations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Attestations.UpdateAttestation
{
    [ModuleOperation(permission: PermissionCodes.ATTESTATIONS_GENERAL_ACCESS)]

    public class UpdateAttestationCommand : IRequest<Unit>
    {
        public AddEditAttestationDto Data { get; set; }
    }
}
