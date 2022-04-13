using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities.Documents
{
    public class DocumentTemplate : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public FileTypeEnum FileType { get; set; }
    }
}
