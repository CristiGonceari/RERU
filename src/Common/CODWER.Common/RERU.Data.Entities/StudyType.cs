using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class StudyType : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public int TrasnlateId { get; set; }
    }
}
