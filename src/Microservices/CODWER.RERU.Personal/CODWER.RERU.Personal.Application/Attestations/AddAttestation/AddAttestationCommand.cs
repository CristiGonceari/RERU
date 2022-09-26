using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Attestations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Attestations.AddAttestation
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ATESTARI)]
    public class AddAttestationCommand : IRequest<int>
    {
        public AddEditAttestationDto Data { get; set; }
    }
}
