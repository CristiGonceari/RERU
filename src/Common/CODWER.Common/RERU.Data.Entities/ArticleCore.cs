using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class ArticleCore : SoftDeleteBaseEntity
    {
        public ArticleCore()
        {
            ArticleRoles = new HashSet<ArticleCoreModuleRole>();
        }

        public string Name { get; set; }
        public string Content { get; set; }
        public string MediaFileId { get; set; }

        public virtual ICollection<ArticleCoreModuleRole> ArticleRoles { set; get; }
    }
}
