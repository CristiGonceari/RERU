using System.Collections.Generic;
using System.Linq;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePositions
{
    public class GetCandidatePositionsQueryHandler : IRequestHandler<GetCandidatePositionsQuery, PaginatedModel<CandidatePositionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly ICandidatePositionService _candidatePositionService;

        public GetCandidatePositionsQueryHandler(AppDbContext appDbContext, 
            IPaginationService paginationService, 
            ICandidatePositionService candidatePositionService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _candidatePositionService = candidatePositionService;
        }

        public async Task<PaginatedModel<CandidatePositionDto>> Handle(GetCandidatePositionsQuery request, CancellationToken cancellationToken)
        {
            var positions = GetAndPrintCandidatePosition.Filter(_appDbContext, request.Name);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<CandidatePosition, CandidatePositionDto>(positions, request);

            paginatedModel = await GetResponsiblePersonName(paginatedModel, positions);

            return paginatedModel;
        }

        private async Task<PaginatedModel<CandidatePositionDto>> GetResponsiblePersonName(PaginatedModel<CandidatePositionDto> paginatedModel, IEnumerable<CandidatePosition> candidatePositions)
        {
            var positions = candidatePositions.ToList();

            foreach (var item in paginatedModel.Items)
            {
                var position = positions.FirstOrDefault(x => x.Id == item.Id);

                item.ResponsiblePerson = _candidatePositionService.GetResponsiblePersonName(int.Parse(position?.CreateById ?? "0"));
                item.ResponsiblePersonId = int.Parse(position?.CreateById ?? "0");
            }

            return paginatedModel;
        }

    }
}
