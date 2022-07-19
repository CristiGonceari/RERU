using System.Linq;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using CVU.ERP.StorageService;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.SubordinateRequests.GetRequests
{
    public class GetSubordinateRequestsQueryHandler : IRequestHandler<GetSubordinateRequestsQuery, PaginatedModel<DismissalRequestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IPaginationService _paginationService;
        private readonly IStorageFileService _storageFileService;

        public GetSubordinateRequestsQueryHandler(AppDbContext appDbContext, 
            IUserProfileService userProfileService, 
            IPaginationService paginationService, 
            IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _paginationService = paginationService;
            _storageFileService = storageFileService;
        }

        public async Task<PaginatedModel<DismissalRequestDto>> Handle(GetSubordinateRequestsQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.DismissalRequests
                .Include(x => x.Contractor)
                    .ThenInclude(c=>c.Contracts)
                .Include(x => x.Contractor)
                    .ThenInclude(c => c.Positions)
                        .ThenInclude(p=>p.Role)
                .Where(x => x.Contractor.Contracts.Any(c => c.SuperiorId == contractorId));

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<DismissalRequest, DismissalRequestDto>(items, request);

            paginatedModel = await GetOrderAndRequestName(paginatedModel);

            return paginatedModel;
        }

        private async Task<PaginatedModel<DismissalRequestDto>> GetOrderAndRequestName(PaginatedModel<DismissalRequestDto> paginatedModel)
        {
            foreach (var item in paginatedModel.Items)
            {
                item.OrderName = await _storageFileService.GetFileName(item.OrderId);
                item.RequestName = await _storageFileService.GetFileName(item.RequestId);
            }

            return paginatedModel;
        }
    }
}
