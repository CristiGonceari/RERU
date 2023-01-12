using RERU.Data.Entities.Enums;

namespace CVU.ERP.Module.Application.ImportProcessServices.ImportProcessModels
{
    public class ProcessDataDto
    {
        public int Done { get; set; }
        public int Total { get; set; }
        public ProcessesEnum ProcessesEnumType { get; set; }
        public bool IsDone { get; set; }
        public string FileId { get; set; }
    }
}
