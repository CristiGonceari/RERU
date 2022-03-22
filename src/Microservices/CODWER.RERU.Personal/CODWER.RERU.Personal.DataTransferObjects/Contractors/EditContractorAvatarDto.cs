using CVU.ERP.StorageService.Models;

namespace CODWER.RERU.Personal.DataTransferObjects.Contractors
{
    public class EditContractorAvatarDto
    {
        public int ContractorId { get; set; }
        public string? MediaFileId { get; set; }
        public AddFileDto FileDto { get; set; }
    }
}
