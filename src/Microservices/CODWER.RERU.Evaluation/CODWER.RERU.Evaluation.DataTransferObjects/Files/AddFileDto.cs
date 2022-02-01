using CODWER.RERU.Evaluation.Data.Entities.Files;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Files
{
    public class AddFileDto
    {
        public IFormFile File { get; set; }
        public FileTypeEnum Type { get; set; }
    }
}
