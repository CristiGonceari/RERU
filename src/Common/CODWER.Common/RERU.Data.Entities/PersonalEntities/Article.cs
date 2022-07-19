using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities
{
    public class Article : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
