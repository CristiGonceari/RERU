using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorPositions.GetContractorPositions
{
    public class GetContractorPositionsQueryHandler : IRequestHandler<GetContractorPositionsQuery, PaginatedModel<PositionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;
        private readonly IStorageFileService _storageFileService;

        public GetContractorPositionsQueryHandler(AppDbContext appDbContext,
            IPaginationService paginationService,
            IUserProfileService userProfileService,
            IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
            _storageFileService = storageFileService;
        }

        public async Task<PaginatedModel<PositionDto>> Handle(GetContractorPositionsQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.Positions
                .Include(x => x.Department)
                .Include(x => x.OrganizationRole)
                .Include(x => x.Contractor)
                .Where(x => x.ContractorId == contractorId);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Position, PositionDto>(items, request);

            paginatedModel = await GetOrderName(paginatedModel);

            return paginatedModel;
        }
        private async Task<PaginatedModel<PositionDto>> GetOrderName(PaginatedModel<PositionDto> paginatedModel)
        {
            foreach (var item in paginatedModel.Items)
            {
                item.OrderName = await _storageFileService.GetFileName(item.OrderId);
            }

            return paginatedModel;
        }
    }
}
