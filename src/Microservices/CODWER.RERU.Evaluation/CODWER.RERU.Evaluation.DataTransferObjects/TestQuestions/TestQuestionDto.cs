using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions
{
    public class TestQuestionDto
    {
        public int Id { get; set; }
        public int QuestionCategoryId { get; set; }
        public int QuestionUnitId { get; set; }
        public int AnswersCount { get; set; }
        public int? TimeLimit { get; set; }
        public string Question { get; set; }
        public string CategoryName { get; set; }
        public string AnswerText { get; set; }

        public AnswerStatusEnum AnswerStatus { get; set; }
        public QuestionTypeEnum QuestionType { get; set; }
        public List<TestOptionsDto> Options { get; set; }
        public List<TestOptionsDto> HashedOptions { get; set; }
    }
}
