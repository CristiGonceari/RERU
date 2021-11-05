namespace CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests
{
    public class TestVerificationProgressDto
    {
        public int TestId { get; set; }
        public int TotalQuestions { get; set; }
        public int VerifiedBySystemCount { get; set; }
        public int VerifiedByUserCount { get; set; }
    }
}
