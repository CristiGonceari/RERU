using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.DocumentForSign
{
    public class DocumentForSignDto
    {
        public int DocumentForSignId { get; set; }
        public string FileName { get; set; }
        public string MediaFileId { get; set; }

        public List<SignedDocumentDto> SignedDocuments { get; set; }
    }
}