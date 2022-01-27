using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Vacations.GetMyVacations
{
    [ModuleOperation(permission: PermissionCodes.VACATIONS_GENERAL_ACCESS)]

    public class GetMyVacationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<VacationDto>>
    {
        //public int? ContractorId { get; set; }
    }
}
