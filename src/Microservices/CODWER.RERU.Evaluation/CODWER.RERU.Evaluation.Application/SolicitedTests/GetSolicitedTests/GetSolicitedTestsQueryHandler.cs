using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;


namespace CODWER.RERU.Evaluation.Application.SolicitedTests.GetSolicitedTests
{
    public class GetSolicitedTestsQueryHandler : IRequestHandler<GetSolicitedTestsQuery, PaginatedModel<SolicitedCandidatePositionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetSolicitedTestsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<SolicitedCandidatePositionDto>> Handle(GetSolicitedTestsQuery request, CancellationToken cancellationToken)
        {
            var solicitedTests = GetAndFilterSolicitedTests.Filter(_appDbContext, request.PositionId, request.UserName, request.Status, request.FromDate, request.TillDate);

            return await _paginationService.MapAndPaginateModelAsync<SolicitedVacantPosition, SolicitedCandidatePositionDto>(solicitedTests, request);
        }
    }
}
