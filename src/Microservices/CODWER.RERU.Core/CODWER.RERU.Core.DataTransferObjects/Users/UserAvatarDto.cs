using CVU.ERP.StorageService.Models;

namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class UserAvatarDto
    {
        public int UserId { get; set; }
        public AddFileDto File { get; set; }
    }
}
