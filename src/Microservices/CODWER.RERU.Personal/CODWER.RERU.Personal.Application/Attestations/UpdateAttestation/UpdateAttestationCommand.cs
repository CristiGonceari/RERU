using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Attestations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Attestations.UpdateAttestation
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ATESTARI)]

    public class UpdateAttestationCommand : IRequest<Unit>
    {
        public AddEditAttestationDto Data { get; set; }
    }
}
