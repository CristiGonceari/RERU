using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Positions.GetPositions
{
    public class GetPositionsHandler : IRequestHandler<GetPositionsQuery, PaginatedModel<PositionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetPositionsHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<PositionDto>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Positions
                .Include(x => x.Department)
                .Include(x => x.OrganizationRole)
                .Include(x => x.Contractor)
                .Include(x => x.Order)
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

            return paginatedModel;
        }
    }
}
