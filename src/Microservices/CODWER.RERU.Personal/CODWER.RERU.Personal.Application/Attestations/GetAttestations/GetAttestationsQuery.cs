using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Attestations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Attestations.GetAttestations
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ATESTARI)]
    public class GetAttestationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<AttestationDto>>
    {
        public int? ContractorId { get; set; }
    }
}
