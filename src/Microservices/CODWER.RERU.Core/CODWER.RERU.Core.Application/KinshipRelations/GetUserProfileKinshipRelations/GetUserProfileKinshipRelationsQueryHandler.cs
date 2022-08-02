using CODWER.RERU.Core.DataTransferObjects.KinshipRelation;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelations.GetUserProfileKinshipRelations
{
    public class GetUserProfileKinshipRelationsQueryHandler : IRequestHandler<GetUserProfileKinshipRelationsQuery, PaginatedModel<KinshipRelationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetUserProfileKinshipRelationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }
        public async Task<PaginatedModel<KinshipRelationDto>> Handle(GetUserProfileKinshipRelationsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.KinshipRelations
                 .Where(x => x.ContractorId == request.ContractorId)
                 .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<KinshipRelation, KinshipRelationDto>(items, request);

            return paginatedModel;
        }
    }
}
