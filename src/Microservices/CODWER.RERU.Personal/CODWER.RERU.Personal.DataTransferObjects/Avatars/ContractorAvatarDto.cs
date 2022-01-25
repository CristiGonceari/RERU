using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.DataTransferObjects.Avatars
{
    public class ContractorAvatarDto
    {
        public int ContractorId { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
