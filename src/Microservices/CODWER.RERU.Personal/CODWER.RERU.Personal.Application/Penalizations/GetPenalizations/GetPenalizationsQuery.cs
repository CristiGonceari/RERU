using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Penalizations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Penalizations.GetPenalizations
{
    [ModuleOperation(permission: PermissionCodes.PENALIZATIONS_GENERAL_ACCESS)]

    public class GetPenalizationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PenalizationDto>>
    {
        public int? ContractorId { get; set; }
    }
}
