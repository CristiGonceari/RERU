using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CVU.ERP.Module.Application.ImportProcessServices.ImportProcessModels;

namespace CVU.ERP.Module.Application.ImportProcessServices.Implementations
{
    public class ImportProcessService : IImportProcessService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IStorageFileService _storageFileService;

        public ImportProcessService(AppDbContext appDbContext, 
            IMapper mapper, 
            IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _storageFileService = storageFileService;
        }

        public async Task<ProcessDataDto> GetImportProcess(int processId)
        {
            var process = await _appDbContext.Processes.FirstOrDefaultAsync(x => x.Id == processId);

            return _mapper.Map<ProcessDataDto>(process);
        }

        public async Task<FileDataDto> GetImportResult(string fileId)
        {
            return await _storageFileService.GetFile(fileId);
        }

        public async Task<int> StartImportProcess(StartProcessDto dto)
        {
            var processes = new Process
            {
                Done = 0,
                Total = dto.TotalProcesses ?? 0,
                ProcessesEnumType = dto.ProcessType,
                IsDone = false
            };

            await _appDbContext.Processes.AddAsync(processes);
            await _appDbContext.SaveChangesAsync();

            return processes.Id;
        }

        public async Task<Unit> StopAllProcesses()
        {
            var allProcesses = _appDbContext.Processes.Where(x => x.IsDone == false);

            foreach (var process in allProcesses)
            {
                process.IsDone = true;
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
