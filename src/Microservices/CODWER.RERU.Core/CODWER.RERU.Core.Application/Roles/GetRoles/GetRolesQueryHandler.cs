using System;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.DataTransferObjects.Roles;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedModel<RoleDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetRolesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = GetAndFilterRoles.Filter(_appDbContext, request.Name);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Role, RoleDto>(roles, request);

            return paginatedModel;
        }
    }
}
