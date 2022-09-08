using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class FileTestAnswer : SoftDeleteBaseEntity
    {
        public int TestQuestionId { get; set; }
        public TestQuestion TestQuestion { get; set; }

        public string FileId { get; set; }
    }
}
