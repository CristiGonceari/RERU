using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions
{
    public class TestQuestionSummaryDto
    {
        public int Index { get; set; }
        public AnswerStatusEnum AnswerStatus { get; set; }
        public bool IsClosed { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
    }
}
