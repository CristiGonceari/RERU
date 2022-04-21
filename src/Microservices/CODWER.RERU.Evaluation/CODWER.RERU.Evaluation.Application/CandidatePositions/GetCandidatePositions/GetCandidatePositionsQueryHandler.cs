using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePositions
{
    public class GetCandidatePositionsQueryHandler : IRequestHandler<GetCandidatePositionsQuery, PaginatedModel<CandidatePositionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetCandidatePositionsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<CandidatePositionDto>> Handle(GetCandidatePositionsQuery request, CancellationToken cancellationToken)
        {
            var positions = GetAndPrintCandidatePosition.Filter(_appDbContext, request.Name);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<CandidatePosition, CandidatePositionDto>(positions, request);

            return paginatedModel;
        }
    }
}
