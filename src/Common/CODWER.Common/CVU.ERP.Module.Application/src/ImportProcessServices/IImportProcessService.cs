using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.ImportProcessServices.ImportProcessModels;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CVU.ERP.Module.Application.ImportProcessServices
{
    public interface IImportProcessService
    {
        public Task<ProcessDataDto> GetImportProcess(int processId);
        public Task<FileDataDto> GetImportResult(string fileId);
        public Task<int> StartImportProcess(StartProcessDto dto);
        public Task<Unit> StopAllProcesses();
    }
}
