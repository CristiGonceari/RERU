using CODWER.RERU.Personal.DataTransferObjects.MilitaryObligation;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.MilitaryObligations.GetContractorMilitaryObligations
{
    public class GetContractorMilitaryObligationsQueryHandler : IRequestHandler<GetContractorMilitaryObligationsQuery, PaginatedModel<MilitaryObligationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetContractorMilitaryObligationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<MilitaryObligationDto>> Handle(GetContractorMilitaryObligationsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.MilitaryObligations
                .Where(x => x.ContractorId == request.ContractorId)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<MilitaryObligation, MilitaryObligationDto>(items, request);

            return paginatedModel;
        }
    }
}
