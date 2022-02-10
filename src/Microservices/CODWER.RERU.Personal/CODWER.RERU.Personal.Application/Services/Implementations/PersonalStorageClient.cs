using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> HasEmploymentRequest(int id)
        {
            var contractor = await GetContractor(id);

            var files = _storageDbContext.Files.ToList()
                .Where(x => contractor.Any(i => i.FileId == x.Id.ToString())).Any(x => x.FileType == FileTypeEnum.request);

            return files;
        }

        public async Task<bool> HasIdentityDocuments(int id)
        {
            var contractor = await GetContractor(id);

            var files = _storageDbContext.Files.ToList()
                .Where(x => contractor.Any(i => i.FileId == x.Id.ToString())).Any(x => x.FileType == FileTypeEnum.identityfiles);

            return files;
        }

        public async Task<IQueryable<File>> GetContractorFiles(int id)
        {
            var contractor = await _appDbContext.ContractorFiles
                .Where(x => x.ContractorId == id).ToListAsync();

            var files = _storageDbContext.Files.Where(file => contractor.All(contractorFile => contractorFile.FileId == file.Id.ToString()));

            return files;
        }

        private async Task<List<ContractorFile>> GetContractor(int id)
        {
           return await _appDbContext.ContractorFiles
                .Where(x => x.ContractorId == id).ToListAsync();
        }
    }
}
