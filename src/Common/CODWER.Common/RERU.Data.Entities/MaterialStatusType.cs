using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class MaterialStatusType : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public int TranslateId { get; set; }
    }
}
