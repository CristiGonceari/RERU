using System.Linq;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Personal.DataTransferObjects.Studies;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.Data.Entities.Studies;

namespace CODWER.RERU.Personal.Application.Studies.GetContractorStudies
{
    public class GetContractorStudiesQueryHandler : IRequestHandler<GetContractorStudiesQuery, PaginatedModel<StudyDataDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetContractorStudiesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<StudyDataDto>> Handle(GetContractorStudiesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Studies
                .Include(x => x.StudyType)
                .Where(x => x.ContractorId == request.ContractorId)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Study, StudyDataDto>(items, request);

            return paginatedModel;
        }
    }
}
