using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRolePermissions.UpdateModuleRolePermissions
{
    public class UpdateModuleRolePermissionsCommandHandler : BaseHandler, IRequestHandler<UpdateModuleRolePermissionsCommand, Unit>
    {

        public UpdateModuleRolePermissionsCommandHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider) { }

        public async Task<Unit> Handle(UpdateModuleRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            var moduleRolePermissions = await CoreDbContext.ModuleRolePermissions
                .Where(mrp => mrp.ModuleRoleId == request.ModuleRolePermissions.ModuleRoleId)
                .ToListAsync();

            foreach(var permission in request.ModuleRolePermissions.Permissions)
            {
                if (permission.IsGranted) 
                { 
                    if(!moduleRolePermissions.Any(x=>x.ModulePermissionId == permission.PermissionId))
                    {
                        CoreDbContext.ModuleRolePermissions.Add(new Data.Entities.ModuleRolePermission 
                        {
                            ModuleRoleId = request.ModuleRolePermissions.ModuleRoleId,
                            ModulePermissionId = permission.PermissionId
                        });
                    }
                }
                else
                {
                    if(moduleRolePermissions.Any(x => x.ModulePermissionId == permission.PermissionId))
                    {
                        var permissionToDelete = moduleRolePermissions.First(x => x.ModulePermissionId == permission.PermissionId);
                        CoreDbContext.ModuleRolePermissions.Remove(permissionToDelete);
                    }
                }

                await CoreDbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}