using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.DocumentForSign
{
    public class SignedDocumentDto
    {
        public int UserProfileId { get; set; }
        public string FullName { get; set; }
        public string SignRequestId { get; set; }
        public SignRequestStatusEnum? Status { get; set; }
    }
}
