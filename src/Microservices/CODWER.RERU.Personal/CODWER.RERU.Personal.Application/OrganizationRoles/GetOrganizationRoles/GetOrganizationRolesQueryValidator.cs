using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRoles
{
    public class GetOrganizationRolesHandler : IRequestHandler<GetOrganizationRolesQuery, PaginatedModel<OrganizationRoleDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetOrganizationRolesHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<OrganizationRoleDto>> Handle(GetOrganizationRolesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.OrganizationRoles
                .OrderBy(x=>x.Name)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchWord))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.SearchWord.ToLower()) 
                                         || x.Code.Contains(request.SearchWord.ToLower()) 
                                         || x.ShortCode.Contains(request.SearchWord.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Code))
            {
                items = items.Where(x => x.Code.Contains(request.Code));
            }

            if (!string.IsNullOrEmpty(request.ShortCode))
            {
                items = items.Where(x => x.ShortCode.Contains(request.ShortCode));
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<OrganizationRole, OrganizationRoleDto>(items, request);

            return paginatedModel;
        }
    }
}
