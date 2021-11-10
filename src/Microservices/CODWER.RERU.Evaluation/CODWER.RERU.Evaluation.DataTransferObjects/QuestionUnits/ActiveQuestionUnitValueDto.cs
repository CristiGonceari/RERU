using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits
{
    public class ActiveQuestionUnitValueDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public QuestionUnitStatusEnum Status { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public int CategoryId { get; set; }
    }
}
