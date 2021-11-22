using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRolePermissions.GetModuleRolePermissionsForUpdate
{
    public class GetModuleRolePermissionsForUpdateQueryHandler : BaseHandler, IRequestHandler<GetModuleRolePermissionsForUpdateQuery, List<ModuleRolePermissionGrantedRowDto>>
    {
        public GetModuleRolePermissionsForUpdateQueryHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider){ }

        public async Task<List<ModuleRolePermissionGrantedRowDto>> Handle(GetModuleRolePermissionsForUpdateQuery request, CancellationToken cancellationToken)
        {
            var moduleRolePermissions = await CoreDbContext.ModuleRolePermissions
                .Where(mrp => mrp.ModuleRoleId == request.RoleId)
                .ToListAsync();

            var module = CoreDbContext.ModuleRoles
                .FirstOrDefault(mrp => mrp.Id == request.RoleId);

            var modulePermissions = await CoreDbContext.ModulePermissions
                .Where(m => m.ModuleId == module.ModuleId)
                .ToListAsync();

            List<ModuleRolePermissionGrantedRowDto> allPerm = new List<ModuleRolePermissionGrantedRowDto>();

            foreach (var permission in modulePermissions)
            {
                if (moduleRolePermissions.Any(x => x.ModulePermissionId == permission.Id))
                {
                    var perm2 = Mapper.Map<ModuleRolePermissionGrantedRowDto>(permission);
                    perm2.IsGranted = true;
                    allPerm.Add(perm2);
                }
                else
                {
                    var perm2 = Mapper.Map<ModuleRolePermissionGrantedRowDto>(permission);
                    perm2.IsGranted = false;
                    allPerm.Add(perm2);
                }
            }
            return allPerm;
        }
    }
}