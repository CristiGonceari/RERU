using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities.Documents
{
    public class DocumentTemplate : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public FileTypeEnum FileType { get; set; }
    }
}
