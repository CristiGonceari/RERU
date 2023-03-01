using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.ModuleRoles
{
    public class ModuleRoleRowDto
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public RoleTypeEnum Type { set; get; }
        public bool IsAssignByDefault { set; get; }
    }
}
