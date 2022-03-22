using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Core.Data.Entities
{
    public class Article : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
