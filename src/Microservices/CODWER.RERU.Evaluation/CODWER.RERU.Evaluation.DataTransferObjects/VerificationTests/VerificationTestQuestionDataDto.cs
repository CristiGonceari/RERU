using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests
{
    public class VerificationTestQuestionDataDto
    {
        public List<VerificationTestQuestionSummaryDto> TestQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public int Points { get; set; }
        public int Result { get; set; }
    }
}
