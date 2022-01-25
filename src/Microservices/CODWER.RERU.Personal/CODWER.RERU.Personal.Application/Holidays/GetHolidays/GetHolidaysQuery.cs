using CODWER.RERU.Personal.DataTransferObjects.Holidays;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Holidays.GetHolidays
{
    public class GetHolidaysQuery : PaginatedQueryParameter, IRequest<PaginatedModel<HolidayDto>>
    {
    }
}
