using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Data.Entities;
using CVU.ERP.Module.Common.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Modules.UpdateSelfAsModule 
{
    public class UpdateSelfAsModuleCommandHandler : BaseHandler, IRequestHandler<UpdateSelfAsModuleCommand, Unit> 
    {
        private IEnumerable<IModulePermissionProvider> _permissionProviders;

        public UpdateSelfAsModuleCommandHandler (
            ICommonServiceProvider commonServiceProvider,
            IEnumerable<IModulePermissionProvider> permissionProviders
        ) : base (commonServiceProvider) {
            _permissionProviders = permissionProviders;
        }

        public async Task<Unit> Handle (UpdateSelfAsModuleCommand request, CancellationToken cancellationToken) 
        {
            var moduleCode = "00";
            var superAdministratorCode = "R00000001";
            var selfModule = await CoreDbContext.Modules
                .Include (m => m.Permissions)
                .Include (m => m.Roles)
                .ThenInclude (mr => mr.Permissions)
                .FirstOrDefaultAsync (m => m.Code == moduleCode);

            if (selfModule == null) {
                selfModule = new Data.Entities.Module ();
                selfModule.Code = moduleCode;
                selfModule.Name = "Core/Dashboard";
                selfModule.Permissions = new List<ModulePermission> ();
                selfModule.Roles = new List<ModuleRole> ();
                selfModule.Type = Data.Entities.Enums.ModuleTypeEnum.Default;
                CoreDbContext.Modules.Add (selfModule);
            }

            var allModulePermissions = new List<CVU.ERP.Module.Common.Models.ModulePermission> ();

            foreach (var permissionProvider in _permissionProviders) {
                allModulePermissions.AddRange (await permissionProvider.Get ());
            }

            selfModule.Permissions.RemoveAll (dbPermission => allModulePermissions.All (modulePermission => dbPermission.Code != modulePermission.Code));
            selfModule.Permissions.ForEach ((dbPermission) => { dbPermission.Description = allModulePermissions.First (modulePermission => dbPermission.Code == modulePermission.Code).Description; });
            selfModule.Permissions.AddRange (Mapper.Map<IEnumerable<ModulePermission>> (allModulePermissions.Where (modulePermission => selfModule.Permissions.All (dbPermission => dbPermission.Code != modulePermission.Code))));

            var superAdministratorRole = selfModule.Roles.FirstOrDefault (r => r.Code == superAdministratorCode);
            if (superAdministratorRole == null) {
                superAdministratorRole = new ModuleRole ();
                superAdministratorRole.Code = superAdministratorCode;
                superAdministratorRole.Type = Data.Entities.Enums.RoleTypeEnum.Default;
                superAdministratorRole.Name = "Super administrator";
                superAdministratorRole.Permissions = new List<ModuleRolePermission> ();
                selfModule.Roles.Add (superAdministratorRole);
            }

            superAdministratorRole.Permissions.AddRange (selfModule.Permissions.Where (mp => superAdministratorRole.Permissions.All (mrp => mrp.Permission.Code != mp.Code)).Select (mp => new ModuleRolePermission { Permission = mp }));
            await CoreDbContext.SaveChangesAsync ();

            await AddAdministratorUserProfile();

            return Unit.Value;
        }

        private async Task AddAdministratorUserProfile()
        {
            if (!CoreDbContext.UserProfileModuleRoles.Any())
            {
                var userProfileModuleRoles = new UserProfileModuleRole
                {
                    UserProfile = new UserProfile(),
                    ModuleRoleId = 1
                };


                userProfileModuleRoles.UserProfile.Name = "Administrator";
                userProfileModuleRoles.UserProfile.LastName = "Platforma";
                userProfileModuleRoles.UserProfile.IsActive = true;
                userProfileModuleRoles.UserProfile.RequiresDataEntry = true;
                userProfileModuleRoles.UserProfile.Identities.Add(new UserProfileIdentity { Identificator = UserManagementDbContext.Users.First().Id, Type = "local" });

                CoreDbContext.UserProfileModuleRoles.Add(userProfileModuleRoles);
                await CoreDbContext.SaveChangesAsync();
            }
        }
    }
}