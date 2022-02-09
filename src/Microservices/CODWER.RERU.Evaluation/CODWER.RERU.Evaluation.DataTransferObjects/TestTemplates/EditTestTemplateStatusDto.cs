using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTypes
{
    public class EditTestTemplateStatusDto
    {
        public int TestTypeId { get; set; }
        public TestTypeStatusEnum Status { get; set; }
    }
}
