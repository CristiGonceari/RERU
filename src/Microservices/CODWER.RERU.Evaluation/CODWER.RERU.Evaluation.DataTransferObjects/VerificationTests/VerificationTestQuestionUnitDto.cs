using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests
{
    public class VerificationTestQuestionUnitDto
    {
        public int Id { get; set; }
        public int QuestionCategoryId { get; set; }
        public int QuestionUnitId { get; set; }
        public int AnswersCount { get; set; }
        public int Verified { get; set; }
        public string Question { get; set; }
        public string CorrectHashedQuestion { get; set; }
        public string CategoryName { get; set; }
        public string AnswerText { get; set; }
        public string Comment { get; set; }
        public bool? IsCorrect { get; set; }
        public int QuestionMaxPoints { get; set; }
        public int EvaluatorPoints { get; set; }
        public string QuestionUnitMediaFileId { get; set; }
        public bool? ShowNegativeMessage { get; set; }

        public QuestionTypeEnum QuestionType { get; set; }
        public List<VerificationTestOptionsDto> Options { get; set; }
        public List<VerificationTestOptionsDto> HashedOptions { get; set; }
    }
}
