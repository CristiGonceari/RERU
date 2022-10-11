using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class ArticleEvaluation : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string MediaFileId { get; set; }
    }
}
