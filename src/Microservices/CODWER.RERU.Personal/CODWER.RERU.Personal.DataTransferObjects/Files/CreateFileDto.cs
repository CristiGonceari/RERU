using CVU.ERP.StorageService.Entities;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.DataTransferObjects.Files
{
    public class CreateFileDto
    {
        public int ContractorId { get; set; }
        public IFormFile File { get; set; }
        public FileTypeEnum Type { get; set; }
    }
}
