using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates
{
    public class EditTestTemplateStatusDto
    {
        public int TestTemplateId { get; set; }
        public TestTypeStatusEnum Status { get; set; }
    }
}
