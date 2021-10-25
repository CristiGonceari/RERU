
namespace CODWER.RERU.Core.DataTransferObjects.ModuleRoles
{
    public class AddEditModuleRoleDto
    {
        public int Id { get; set; }
        public int? ModuleId { set; get; }
        public string Name { set; get; }
        public bool IsAssignByDefault { set; get; }
    }
}
