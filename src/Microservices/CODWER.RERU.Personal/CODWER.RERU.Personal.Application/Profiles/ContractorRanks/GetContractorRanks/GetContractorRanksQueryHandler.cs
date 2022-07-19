using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Ranks;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorRanks.GetContractorRanks
{
    public class GetContractorRanksQueryHandler : IRequestHandler<GetContractorRanksQuery, PaginatedModel<RankDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetContractorRanksQueryHandler(AppDbContext appDbContext, 
            IPaginationService paginationService, 
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<RankDto>> Handle(GetContractorRanksQuery request, CancellationToken cancellationToken)
        {
            var currentContractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.Ranks
                .Include(x => x.RankRecord)
                .Where(x => x.ContractorId == currentContractorId)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Rank, RankDto>(items, request);

            return paginatedModel;
        }
    }
}
