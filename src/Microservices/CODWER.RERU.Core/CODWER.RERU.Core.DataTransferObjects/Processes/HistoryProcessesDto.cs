using System;

namespace CODWER.RERU.Core.DataTransferObjects.Processes
{
    public class HistoryProcessesDto
    {
        public int Done { get; set; }
        public int Total { get; set; }
        public string FileId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
