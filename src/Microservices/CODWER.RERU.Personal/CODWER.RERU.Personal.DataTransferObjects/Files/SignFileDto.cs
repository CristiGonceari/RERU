using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.DataTransferObjects.Files
{
    public class SignFileDto
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
    }
}
