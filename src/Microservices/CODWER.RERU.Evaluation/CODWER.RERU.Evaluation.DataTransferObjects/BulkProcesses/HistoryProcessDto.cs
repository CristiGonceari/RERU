using System;

namespace CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses
{
    public class HistoryProcessDto
    {
        public int Done { get; set; }
        public int Total { get; set; }
        public string FileId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool isDone { get; set; }
    }
}
