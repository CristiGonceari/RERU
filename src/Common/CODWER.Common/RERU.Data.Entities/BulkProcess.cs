using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class BulkProcess : SoftDeleteBaseEntity
    {
        public int DoneProcesses { get; set; }
        public int TotalProcesses { get; set; }
        public Processes ProcessType { get; set; }
        public bool IsDone { get; set; }
        public string FileId { get; set; }
    }
}
