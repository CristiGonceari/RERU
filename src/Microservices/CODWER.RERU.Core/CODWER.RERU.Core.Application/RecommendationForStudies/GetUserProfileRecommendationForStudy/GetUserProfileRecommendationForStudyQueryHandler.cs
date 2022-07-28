using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.DataTransferObjects.RecommendationForStudy;

namespace CODWER.RERU.Core.Application.RecommendationForStudies.GetUserProfileRecommendationForStudy
{
    public class GetUserProfileRecommendationForStudyQueryHandler : IRequestHandler<GetUserProfileRecommendationForStudyQuery, PaginatedModel<RecommendationForStudyDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetUserProfileRecommendationForStudyQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }
        public async Task<PaginatedModel<RecommendationForStudyDto>> Handle(GetUserProfileRecommendationForStudyQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.RecommendationForStudies.Where(rfs => rfs.UserProfileId == request.UserProfileId);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<RecommendationForStudy, RecommendationForStudyDto>(items, request);

            return paginatedModel;
        }
    }
}
