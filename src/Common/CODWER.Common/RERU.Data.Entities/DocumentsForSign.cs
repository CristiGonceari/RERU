using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using System.Collections.Generic;

namespace RERU.Data.Entities
{
    public class DocumentsForSign : SoftDeleteBaseEntity
    {
        public DocumentsForSign()
        {
            SignedDocuments = new HashSet<SignedDocument>();
        }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string MediaFileId { get; set; }

        public int? TestId { get; set; }
        public Test Test { get; set; }

        public int? TestTemplateId { get; set; }
        public TestTemplate TestTemplate { get; set; }

        public virtual ICollection<SignedDocument> SignedDocuments { get; set; }
    }
}
