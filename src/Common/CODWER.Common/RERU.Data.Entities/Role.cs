using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class Role : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public int ColaboratorId { get; set; }
    }
}
