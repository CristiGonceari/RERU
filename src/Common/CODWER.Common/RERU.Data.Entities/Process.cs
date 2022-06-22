using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class Process : SoftDeleteBaseEntity
    {
        public int Done { get; set; }
        public int Total { get; set; }
        public ProcessesEnum ProcessesEnumType { get; set; }
        public bool IsDone { get; set; }
        public string FileId { get; set; }
    }
}
