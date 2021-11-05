using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits
{
    public class QuestionUnitPreviewDto
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public QuestionUnitStatusEnum Status { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public string Question { get; set; }
        public string CategoryName { get; set; }
    }
}
