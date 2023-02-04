using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class ArticleEv360 : SoftDeleteBaseEntity
    {
        public ArticleEv360()
        {
            ArticleRoles = new HashSet<ArticleEv360ModuleRole>();
        }

        public string Name { get; set; }
        public string Content { get; set; }
        public string MediaFileId { get; set; }

        public virtual ICollection<ArticleEv360ModuleRole> ArticleRoles { set; get; }
    }
}