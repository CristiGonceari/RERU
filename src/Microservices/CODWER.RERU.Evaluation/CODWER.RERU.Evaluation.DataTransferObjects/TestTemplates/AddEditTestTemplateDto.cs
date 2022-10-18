using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates
{
    public class AddEditTestTemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public int? MinPercent { get; set; }
        public int? Duration { get; set; }
        public TestTemplateModeEnum Mode { get; set; }
        public QualifyingTypeEnum QualifyingType { get; set; }
    }
}