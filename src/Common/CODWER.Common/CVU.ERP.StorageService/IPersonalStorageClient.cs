using CVU.ERP.StorageService.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVU.ERP.StorageService
{
    public interface IPersonalStorageClient
    {
        public Task<bool> HasEmploymentRequest(int id);
        public Task<bool> HasIdentityDocuments(int id);
        public Task<IQueryable<File>> GetContractorFiles(int id);
    }
}
