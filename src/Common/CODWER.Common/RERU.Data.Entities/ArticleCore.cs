using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class ArticleCore : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
