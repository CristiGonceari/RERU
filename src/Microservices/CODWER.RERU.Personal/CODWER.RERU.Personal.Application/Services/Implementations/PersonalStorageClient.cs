using RERU.Data.Entities.PersonalEntities.Files;
using RERU.Data.Persistence.Context;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class PersonalStorageClient : IPersonalStorageClient
    {
        private readonly StorageDbContext _storageDbContext;
        private readonly AppDbContext _appDbContext;

        public PersonalStorageClient(StorageDbContext storageDbContext, AppDbContext appDbContext)
        {
            _storageDbContext = storageDbContext;
            _appDbContext = appDbContext;
        }

        public async Task<bool> HasFile(int contractorId, FileTypeEnum fileType)
        {
            var contractorFilesList = await GetContractorFiles(contractorId);

            return _storageDbContext.Files.ToList()
                .Where(x => contractorFilesList.Any(contractorFile => contractorFile.FileId == x.Id.ToString()))
                .Any(x => x.FileType == fileType); 
        }

        public async Task<IQueryable<File>> GetContractorFiles(List<string> fileIdList)
        {
            return _storageDbContext.Files.Where(file => fileIdList.Contains(file.Id.ToString()));
        }

        public async Task<int> AddFileToContractor(int contractorId, string fileId)
        {
            var contractorFile = new ContractorFile(contractorId, fileId);

            await _appDbContext.ContractorFiles.AddAsync(contractorFile);
            await _appDbContext.SaveChangesAsync();

            return contractorFile.Id;
        }

        private async Task<List<ContractorFile>> GetContractorFiles(int contractorId)
        {
           return await _appDbContext.ContractorFiles
                .Where(x => x.ContractorId == contractorId).ToListAsync();
        }
    }
}
