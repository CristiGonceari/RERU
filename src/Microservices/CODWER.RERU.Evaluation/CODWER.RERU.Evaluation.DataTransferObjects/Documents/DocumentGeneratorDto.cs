using CVU.ERP.StorageService.Entities;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Documents
{
    public class DocumentGeneratorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public FileTypeEnum FileType { get; set; }
    }
}
