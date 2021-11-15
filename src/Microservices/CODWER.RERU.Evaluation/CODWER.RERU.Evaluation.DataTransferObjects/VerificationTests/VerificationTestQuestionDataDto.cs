using System.Collections.Generic;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests
{
    public class VerificationTestQuestionDataDto
    {
        public List<VerificationTestQuestionSummaryDto> TestQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public int Points { get; set; }
        public TestResultStatusEnum Result { get; set; }
    }
}
