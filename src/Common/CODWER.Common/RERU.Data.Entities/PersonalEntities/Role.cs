using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities
{
    public class Role : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public int ColaboratorId { get; set; }
        public string Code { get; set; }
        public string ShortCode { get; set; }
    }
}
