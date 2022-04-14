using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Documents
{
    public class DocumentTemplateKeyDto
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string Description { get; set; }

        public FileTypeEnum FileType { get; set; }
    }
}
