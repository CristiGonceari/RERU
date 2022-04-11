using CVU.ERP.StorageService.Models;

namespace CODWER.RERU.Core.DataTransferObjects.Files
{
    public class AddEditUserFileDto
    {
        public int UserId { get; set; }
        public AddFileDto File { get; set; }
    }
}
