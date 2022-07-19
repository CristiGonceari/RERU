using CVU.ERP.Common.Data.Entities;
using CVU.ERP.StorageService.Entities;

namespace RERU.Data.Entities.PersonalEntities.Documents
{
    public class HrDocumentTemplate : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public FileTypeEnum FileType { get; set; }
    }

}
