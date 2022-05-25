using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses
{
    public class ProcessDataDto
    {
        public int DoneProcesses { get; set; }
        public int TotalProcesses { get; set; }
        public Processes ProcessType { get; set; }
        public bool IsDone { get; set; }
        public string FileId { get; set; }
    }
}
