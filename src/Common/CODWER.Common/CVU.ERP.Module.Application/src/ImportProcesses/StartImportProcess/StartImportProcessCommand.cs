using MediatR;
using RERU.Data.Entities.Enums;

namespace CVU.ERP.Module.Application.ImportProcesses.StartImportProcess
{
    public class StartImportProcessCommand : IRequest<int>
    {
        public int? TotalProcesses { get; set; }
        public ProcessesEnum ProcessType { get; set; }
    }
}
