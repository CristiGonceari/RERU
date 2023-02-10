using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class ArticleEv360ModuleRole : SoftDeleteBaseEntity
    {
        public int RoleId { set; get; }
        public ModuleRole Role { set; get; }

        public int ArticleId { set; get; }
        public ArticleEv360 Article { set; get; }
    }
}