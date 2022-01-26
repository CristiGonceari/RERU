using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetVacations
{
    public class GetVacationsQuery: PaginatedQueryParameter, IRequest<PaginatedModel<MyVacationDto>>
    {
        public VacationType? VacationType { get; set; }
    }
}
