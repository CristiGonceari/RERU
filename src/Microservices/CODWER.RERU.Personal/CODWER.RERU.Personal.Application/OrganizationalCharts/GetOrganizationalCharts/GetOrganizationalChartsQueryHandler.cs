using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationalCharts
{
    public class GetOrganizationalChartsHandler : IRequestHandler<GetOrganizationalChartsQuery, PaginatedModel<OrganizationalChartDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetOrganizationalChartsHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<OrganizationalChartDto>> Handle(GetOrganizationalChartsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.OrganizationalCharts
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<OrganizationalChart, OrganizationalChartDto>(items, request);

            return paginatedModel;
        }
    }
}
