using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests
{
    public class VerificationTestQuestionSummaryDto
    {
        public int Index { get; set; }
        public VerificationStatusEnum VerificationStatus { get; set; }
        public bool IsCorrect { get; set; }
    }
}
