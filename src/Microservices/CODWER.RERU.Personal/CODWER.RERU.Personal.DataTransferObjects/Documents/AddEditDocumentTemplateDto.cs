using CODWER.RERU.Personal.Data.Entities.Files;

namespace CODWER.RERU.Personal.DataTransferObjects.Documents
{
   public class AddEditDocumentTemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public FileTypeEnum FileType { get; set; }
    }
}
