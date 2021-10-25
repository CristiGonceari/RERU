using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModulePermissions.GetAllModulePermissions
{
    public class GetModulePermissionsQueryHandler : BaseHandler, IRequestHandler<GetModulePermissionsQuery, PaginatedModel<ModulePermissionRowDto>>
    {

        private readonly IPaginationService _paginationService;

        public GetModulePermissionsQueryHandler(ICommonServiceProvider commonServiceProvider, IPaginationService paginationService) : base(commonServiceProvider)
        {
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ModulePermissionRowDto>> Handle(GetModulePermissionsQuery request, CancellationToken cancellationToken)
        {
            var moduleRoles = CoreDbContext.ModulePermissions.Where(m => m.ModuleId == request.ModuleId);

            moduleRoles = Filter(moduleRoles, request);

            var paginatedModel = _paginationService.MapAndPaginateModel<Data.Entities.ModulePermission, ModulePermissionRowDto>(moduleRoles, request);

            return paginatedModel;
        }

        private IQueryable<ModulePermission> Filter(IQueryable<ModulePermission> items, GetModulePermissionsQuery request)
        {
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                var toSearch = request.Keyword.Split(' ').ToList();

                foreach(var s in toSearch)
                {
                    items = items.Where(p => p.Code.Contains(s) || p.Description.Contains(s));
                }
            }

            return items;
        }
    }
}