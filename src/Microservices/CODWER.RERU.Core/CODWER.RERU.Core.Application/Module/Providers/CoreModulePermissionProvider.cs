using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.Module.Common.Providers;

namespace CODWER.RERU.Core.Application.Module.Providers {
    public class CoreModulePermissionProvider : IModulePermissionProvider {
        public Task<ModulePermission[]> Get () {
            var permissionList = new List<ModulePermission> ();
            permissionList.Add (new ModulePermission () { Code = Permissions.VIEW_ALL_MODULES, Description = "View all modules" });
            permissionList.Add (new ModulePermission () { Code = Permissions.REGISTER_MODULE, Description = "Register module" });
            permissionList.Add (new ModulePermission () { Code = Permissions.EDIT_MODULE, Description = "Edit module" });
            permissionList.Add (new ModulePermission () { Code = Permissions.VIEW_MODULE_DETAILS, Description = "View module details" });
            permissionList.Add (new ModulePermission () { Code = Permissions.REMOVE_MODULE, Description = "Remove module" });
            permissionList.Add (new ModulePermission () { Code = Permissions.ADD_MODULE_ROLE, Description = "Add module role" });
            permissionList.Add (new ModulePermission () { Code = Permissions.UPDATE_MODULE_ROLE_PERMISSIONS, Description = "Update module role permissions" });
            permissionList.Add (new ModulePermission () { Code = Permissions.EDIT_MODULE_ROLE, Description = "Edit module role" });
            permissionList.Add (new ModulePermission () { Code = Permissions.REMOVE_MODULE_ROLE, Description = "Remove module role" });
            permissionList.Add (new ModulePermission () { Code = Permissions.VIEW_ROLE_PERMISSIONS, Description = "View role permissions" });
            permissionList.Add (new ModulePermission () { Code = Permissions.UPDATE_MODULE_ROLE, Description = "Update module role" });
            permissionList.Add (new ModulePermission () { Code = Permissions.VIEW_ALL_USERS, Description = "View all users" });
            permissionList.Add (new ModulePermission () { Code = Permissions.ADD_USER, Description = "Add user" });
            permissionList.Add (new ModulePermission () { Code = Permissions.EDIT_USER_DETAILS, Description = "Edit user details" });
            permissionList.Add (new ModulePermission () { Code = Permissions.SET_USER_PASSWORD, Description = "Set user password" });
            permissionList.Add (new ModulePermission () { Code = Permissions.RESET_USER_PASSWORD, Description = "Reset user password" });
            permissionList.Add (new ModulePermission () { Code = Permissions.ACTIVATE_USER, Description = "Activate user" });
            permissionList.Add (new ModulePermission () { Code = Permissions.DEACTIVATE_USER, Description = "Deactivate user" });
            permissionList.Add (new ModulePermission () { Code = Permissions.DELETE_USER, Description = "Delete user" });
            permissionList.Add (new ModulePermission () { Code = Permissions.VIEW_USER_DETAILS, Description = "View user details" });
            permissionList.Add (new ModulePermission () { Code = Permissions.VIEW_USER_MODULES, Description = "View user modules" });
            permissionList.Add (new ModulePermission () { Code = Permissions.GIVE_USER_ACCESS_TO_A_MODULE, Description = "Give user access to a module" });
            permissionList.Add (new ModulePermission () { Code = Permissions.REMOVE_USER_ACCESS_TO_A_MODULE, Description = "Remove user access to a module" });
            permissionList.Add (new ModulePermission () { Code = Permissions.UPDATE_USER_MODULE_ACCESS, Description = "Update user module access" });

            return Task.FromResult (permissionList.ToArray ());
        }
    }
}