
namespace CODWER.RERU.Personal.DataTransferObjects.Documents
{
    public class DocumentTemplateKeyDto
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Description { get; set; }

        public int DocumentCategoriesId { get; set; }
    }
}
