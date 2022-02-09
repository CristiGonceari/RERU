using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates
{
    public class TestTemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? QuestionCount { get; set; }
        public int MinPercent { get; set; }
        public int Duration { get; set; }
        public int? CategoriesCount { get; set; }
        public SequenceEnum CategoriesSequence { get; set; }
        public TestTypeStatusEnum Status { get; set; }
        public TestTypeModeEnum Mode { get; set; }
    }
}