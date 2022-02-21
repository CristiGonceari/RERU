using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTemplateQuestionCategories
{
    public class CategoryQuestionUnitDto
    {
        public int Index { get; set; }
        public int QuestionUnitId { get; set; }
        public string Question { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public QuestionUnitStatusEnum Status { get; set; }
        public int OptionsCount { get; set; }
    }
}
