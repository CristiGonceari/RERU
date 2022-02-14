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

namespace CODWER.RERU.Personal.Application.Positions.GetPositions
{
    public class GetPositionsHandler : IRequestHandler<GetPositionsQuery, PaginatedModel<PositionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IStorageFileService _storageFileService;

        public GetPositionsHandler(AppDbContext appDbContext, IPaginationService paginationService, 
            IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _storageFileService = storageFileService;
        }

        public async Task<PaginatedModel<PositionDto>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Positions
                .Include(x => x.Department)
                .Include(x => x.OrganizationRole)
                .Include(x => x.Contractor)
                .AsQueryable();

            if (request.DepartmentId != null)
            {
                items = items.Where(x => x.DepartmentId == request.DepartmentId);
            }

            if (request.OrganizationRoleId != null)
            {
                items = items.Where(x => x.OrganizationRoleId == request.OrganizationRoleId);
            }

            if (request.ContractorId != null)
            {
                items = items.Where(x => x.ContractorId == request.ContractorId);
            }

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
