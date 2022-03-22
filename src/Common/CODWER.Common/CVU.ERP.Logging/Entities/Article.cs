using CVU.ERP.Common.Data.Entities;

namespace CVU.ERP.Logging.Entities
{
    public class Article : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
