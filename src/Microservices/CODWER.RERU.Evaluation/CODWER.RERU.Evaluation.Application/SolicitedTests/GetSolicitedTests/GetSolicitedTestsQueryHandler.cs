using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace CODWER.RERU.Evaluation.Application.SolicitedTests.GetSolicitedTests
{
    public class GetSolicitedTestsQueryHandler : IRequestHandler<GetSolicitedTestsQuery, PaginatedModel<SolicitedTestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetSolicitedTestsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<SolicitedTestDto>> Handle(GetSolicitedTestsQuery request, CancellationToken cancellationToken)
        {
            var solicitedTests = GetAndFilterSolicitedTests.Filter(_appDbContext);

            return await _paginationService.MapAndPaginateModelAsync<SolicitedTest, SolicitedTestDto>(solicitedTests, request);
        }
    }
}
