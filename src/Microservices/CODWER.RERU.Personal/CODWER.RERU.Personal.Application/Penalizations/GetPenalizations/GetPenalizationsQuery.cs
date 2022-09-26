using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Penalizations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Penalizations.GetPenalizations
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_PENALIZARI)]

    public class GetPenalizationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PenalizationDto>>
    {
        public int? ContractorId { get; set; }
    }
}
