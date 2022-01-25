using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.Configurations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Holidays;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Holidays.GetHolidays
{
    public class GetHolidaysQueryHandler : IRequestHandler<GetHolidaysQuery, PaginatedModel<HolidayDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetHolidaysQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<HolidayDto>> Handle(GetHolidaysQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Holidays.AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Holiday,HolidayDto>(items, request);

            return paginatedModel;
        }
    }
}
