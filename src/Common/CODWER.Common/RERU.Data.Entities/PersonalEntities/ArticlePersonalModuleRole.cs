using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities
{
    public class ArticlePersonalModuleRole : SoftDeleteBaseEntity
    {
        public int RoleId { set; get; }
        public ModuleRole Role { set; get; }

        public int ArticleId { set; get; }
        public Article Article { set; get; }
    }
}
