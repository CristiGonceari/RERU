using CODWER.RERU.Core.DataTransferObjects.KinshipRelationWithUserProfile;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.GetKinshipRelationWithUserProfiles
{
    public class GetKinshipRelationWithUserProfilesQueryHandler : IRequestHandler<GetKinshipRelationWithUserProfilesQuery, PaginatedModel<KinshipRelationWithUserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetKinshipRelationWithUserProfilesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<KinshipRelationWithUserProfileDto>> Handle(GetKinshipRelationWithUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.KinshipRelationWithUserProfiles
               .Where(x => x.ContractorId == request.ContractorId)
               .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<KinshipRelationWithUserProfile, KinshipRelationWithUserProfileDto>(items, request);

            return paginatedModel;
        }
    }
}
