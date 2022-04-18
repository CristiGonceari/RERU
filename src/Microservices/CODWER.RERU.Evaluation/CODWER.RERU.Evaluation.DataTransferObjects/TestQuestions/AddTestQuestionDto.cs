using System.Collections.Generic;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions
{
    public class AddTestQuestionDto
    {
        public int TestId { get; set; }
        public int? QuestionIndex { get; set; }
        public int? QuestionUnitId { get; set; }
        public AnswerStatusEnum Status { get; set; }
        public List<TestAnswerDto> Answers { get; set; }
    }
}
