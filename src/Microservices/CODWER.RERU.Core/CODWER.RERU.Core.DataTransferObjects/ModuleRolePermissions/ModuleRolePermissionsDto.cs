
namespace CODWER.RERU.Core.DataTransferObjects.ModuleRolePermissions
{
    public class ModuleRolePermissionsDto
    {
        public int ModuleRoleId;
        public ModuleRolePermissionGrandedDto[] Permissions { set; get; }
    }
}
