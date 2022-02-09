using CVU.ERP.StorageService.Entities;
using Microsoft.AspNetCore.Http;

namespace CVU.ERP.StorageService.Models
{
    public class AddFileDto
    {
        public IFormFile File { get; set; }
        public FileTypeEnum Type { get; set; }
    }
}
