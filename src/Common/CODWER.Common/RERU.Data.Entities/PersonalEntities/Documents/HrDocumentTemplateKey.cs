using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities.Documents
{
    public class HrDocumentTemplateKey : SoftDeleteBaseEntity
    {
        public string KeyName { get; set; }
        public string Description { get; set; }

        public int HrDocumentCategoriesId { get; set; }
        public HrDocumentTemplateCategory HrDocumentCategories { get; set; }
    }
}
