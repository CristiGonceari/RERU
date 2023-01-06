using System;
using System.Linq;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common;

namespace CODWER.RERU.Core.Application.CandidatePositions.GetCandidatePositions
{
    public class GetCandidatePositionsQueryHandler : IRequestHandler<GetCandidatePositionsQuery, PaginatedModel<CandidatePositionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IDateTime _dateTime;

        public GetCandidatePositionsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _dateTime = dateTime;
        }

        public async Task<PaginatedModel<CandidatePositionDto>> Handle(GetCandidatePositionsQuery request, CancellationToken cancellationToken)
        {
            var positions = _appDbContext.CandidatePositions
                .Where(x => x.From <= _dateTime.Now && x.To >= _dateTime.Now && x.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                positions = positions.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<CandidatePosition, CandidatePositionDto>(positions, request);

            return paginatedModel;
        }
    }
}
