using CVU.ERP.StorageService.Entities;

namespace CODWER.RERU.Personal.DataTransferObjects.Documents
{
    public class DocumentTemplateGeneratorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public FileTypeEnum FileType { get; set; }
    }
}
