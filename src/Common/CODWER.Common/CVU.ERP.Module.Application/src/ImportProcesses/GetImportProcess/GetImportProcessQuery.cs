using MediatR;

namespace CVU.ERP.Module.Application.ImportProcesses.GetImportProcess
{
    public class GetImportProcessQuery : IRequest<ProcessDataDto>
    {
        public int ProcessId { get; set; }
    }
}
