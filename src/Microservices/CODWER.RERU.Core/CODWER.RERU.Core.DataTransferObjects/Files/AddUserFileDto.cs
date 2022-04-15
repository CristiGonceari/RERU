using CVU.ERP.StorageService.Models;

namespace CODWER.RERU.Core.DataTransferObjects.Files
{
    public class AddUserFileDto
    {
        public int UserId { get; set; }
        public AddFileDto File { get; set; }
    }
}
