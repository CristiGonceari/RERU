using System;

namespace CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses
{
    public class HistoryProcessDto
    {
        public int DoneProcesses { get; set; }
        public int TotalProcesses { get; set; }
        public string FileId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
