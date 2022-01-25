using CODWER.RERU.Personal.Data.Entities.Files;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.DataTransferObjects.Files
{
    public class AddFileDto
    {
        public int ContractorId { get; set; }
        public IFormFile File { get; set; }
        public FileTypeEnum Type { get; set; }
    }
}
