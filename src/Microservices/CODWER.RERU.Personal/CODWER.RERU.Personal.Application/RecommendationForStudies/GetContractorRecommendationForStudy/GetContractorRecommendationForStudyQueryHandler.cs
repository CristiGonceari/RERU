using CODWER.RERU.Personal.DataTransferObjects.RecommendationForStudy;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies.GetContractorRecommendationForStudy
{
    public class GetContractorRecommendationForStudyQueryHandler : IRequestHandler<GetContractorRecommendationForStudyQuery, PaginatedModel<RecommendationForStudyDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetContractorRecommendationForStudyQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }
        public async Task<PaginatedModel<RecommendationForStudyDto>> Handle(GetContractorRecommendationForStudyQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.RecommendationForStudies.Where(rfs => rfs.ContractorId == request.ContractorId);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<RecommendationForStudy, RecommendationForStudyDto>(items, request);

            return paginatedModel;
        }
    }
}
