using RERU.Data.Entities.Enums;

namespace CVU.ERP.Module.Application.ImportProcessServices.ImportProcessModels
{
    public class StartProcessDto
    {
        public int? TotalProcesses { get; set; }
        public ProcessesEnum ProcessType { get; set; }
    }
}
