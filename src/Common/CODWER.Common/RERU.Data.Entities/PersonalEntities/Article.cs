using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities
{
    public class Article : SoftDeleteBaseEntity
    {
        public Article()
        {
            ArticleRoles = new HashSet<ArticlePersonalModuleRole>();
        }

        public string Name { get; set; }
        public string Content { get; set; }
        public string MediaFileId { get; set; }

        public virtual ICollection<ArticlePersonalModuleRole> ArticleRoles { set; get; }
    }
}
