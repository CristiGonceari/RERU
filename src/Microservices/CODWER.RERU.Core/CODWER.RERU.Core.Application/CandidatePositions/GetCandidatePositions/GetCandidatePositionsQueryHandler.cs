using System.Linq;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;

namespace CODWER.RERU.Core.Application.CandidatePositions.GetCandidatePositions
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
            var positions = _appDbContext.CandidatePositions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                positions = positions.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<CandidatePosition, CandidatePositionDto>(positions, request);

            return paginatedModel;
        }
    }
}
