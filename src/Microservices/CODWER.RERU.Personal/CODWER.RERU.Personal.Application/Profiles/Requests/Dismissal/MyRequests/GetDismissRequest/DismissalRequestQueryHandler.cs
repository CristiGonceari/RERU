using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.MyRequests.GetDismissRequest
{
    public class DismissalRequestQueryHandler : IRequestHandler<DismissalRequestQuery, PaginatedModel<MyDismissalRequestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;
        private readonly IStorageFileService _storageFileService;

        public DismissalRequestQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
            _storageFileService = storageFileService;
        }

        public async Task<PaginatedModel<MyDismissalRequestDto>> Handle(DismissalRequestQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.DismissalRequests
                .Include(x=>x.Position)
                    .ThenInclude(x=>x.Role)
                .Where(x => x.ContractorId == contractorId);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<DismissalRequest, MyDismissalRequestDto>(items, request);

            paginatedModel = await GetOrderAndRequestName(paginatedModel);

            return paginatedModel;
        }

        private async Task<PaginatedModel<MyDismissalRequestDto>> GetOrderAndRequestName(PaginatedModel<MyDismissalRequestDto> paginatedModel)
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
