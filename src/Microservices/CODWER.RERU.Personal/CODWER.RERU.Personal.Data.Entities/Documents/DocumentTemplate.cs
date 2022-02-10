using CVU.ERP.Common.Data.Entities;
using CVU.ERP.StorageService.Entities;

namespace CODWER.RERU.Personal.Data.Entities.Documents
{
    public class DocumentTemplate : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public FileTypeEnum FileType { get; set; }
    }

}
