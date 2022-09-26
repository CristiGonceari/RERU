using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.DismissalRequests.GetDismissalByContractorId
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_CEREREA_DEMISIEI)]
    public class DismissalByContractorIdQuery: PaginatedQueryParameter, IRequest<PaginatedModel<MyDismissalRequestDto>>
    {
        public int ContractorId { get; set; }
    }
}
