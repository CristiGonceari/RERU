using CVU.ERP.StorageService.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVU.ERP.StorageService
{
    public interface IPersonalStorageClient
    {
        public Task<bool> HasFile(int contractorId, FileTypeEnum fileType);
        public Task<IQueryable<File>> GetContractorFiles(List<string> fileIdList);
        public Task<int> AddFileToContractor(int contractorId, string fileId);
    }
}
