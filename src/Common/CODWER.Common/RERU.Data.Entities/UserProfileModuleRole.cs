using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class UserProfileModuleRole : BaseEntity {
        public int UserProfileId { set; get; }
        public int ModuleRoleId { set; get; }

        public UserProfile UserProfile { set; get; }
        public ModuleRole ModuleRole { set; get; }
    }
}