using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.Documents
{
    public class DocumentTemplateKey : SoftDeleteBaseEntity
    {
        public string KeyName { get; set; }
        public string Description { get; set; }

        public int DocumentCategoriesId { get; set; }
        public DocumentTemplateCategory DocumentCategories { get; set; }
    }
}
