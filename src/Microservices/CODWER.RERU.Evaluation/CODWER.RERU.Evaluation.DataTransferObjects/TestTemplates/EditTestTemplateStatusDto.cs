using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates
{
    public class EditTestTemplateStatusDto
    {
        public int TestTemplateId { get; set; }
        public TestTemplateStatusEnum Status { get; set; }
    }
}
