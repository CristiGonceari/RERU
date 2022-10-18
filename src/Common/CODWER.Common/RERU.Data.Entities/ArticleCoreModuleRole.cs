using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class ArticleCoreModuleRole : BaseEntity
    {
        public int RoleId { set; get; }
        public ModuleRole Role { set; get; }

        public int ArticleId { set; get; }
        public ArticleCore Article { set; get; }
    }
}
