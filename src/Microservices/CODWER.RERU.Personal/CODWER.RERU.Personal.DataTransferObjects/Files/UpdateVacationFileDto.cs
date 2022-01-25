using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.DataTransferObjects.Files
{
    public class UpdateVacationFileDto
    {
        public int VacationId { get; set; }
        public IFormFile NewFile { get; set; }
    }
}
