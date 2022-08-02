using CODWER.RERU.Personal.DataTransferObjects.KinshipRelation;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.KinshipRelations.GetContractorKinshipRelations
{
    public class GetContractorKinshipRelationsQueryHandler : IRequestHandler<GetContractorKinshipRelationsQuery, PaginatedModel<KinshipRelationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetContractorKinshipRelationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }
        public async Task<PaginatedModel<KinshipRelationDto>> Handle(GetContractorKinshipRelationsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.KinshipRelations
                 .Where(x => x.ContractorId == request.ContractorId)
                 .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<KinshipRelation, KinshipRelationDto>(items, request);

            return paginatedModel;
        }
    }
}
