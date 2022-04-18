using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.CandidatePositions.GetCandidatePositions
{
    public class GetCandidatePositionsQueryHandler : IRequestHandler<GetCandidatePositionsQuery, PaginatedModel<CandidatePositionDto>>
    {
        private readonly AppDbContext _coreDbContext;
        private readonly IPaginationService _paginationService;

        public GetCandidatePositionsQueryHandler(AppDbContext coreDbContext, IPaginationService paginationService)
        {
            _coreDbContext = coreDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<CandidatePositionDto>> Handle(GetCandidatePositionsQuery request, CancellationToken cancellationToken)
        {
            var positions = _coreDbContext.CandidatePositions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                positions = positions.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<CandidatePosition, CandidatePositionDto>(positions, request);

            return paginatedModel;
        }
    }
}
