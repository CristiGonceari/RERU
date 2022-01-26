using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.SubordinatesVacations.GetVacations
{
    public class GetSubordinateVacationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<SubordinateVacationDto>>
    {
    }
}
