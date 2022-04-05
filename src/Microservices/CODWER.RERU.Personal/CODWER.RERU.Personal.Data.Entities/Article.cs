using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities
{
    public class Article : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
