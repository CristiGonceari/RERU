namespace CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests
{
    public class VerificationTestQuestionDto
    {
        public int TestId { get; set; }
        public int QuestionIndex { get; set; }
        public bool IsCorrect { get; set; }
        public string Comment { get; set; }
        public int EvaluatorPoints { get; set; }
        public int QuestionUnitId { get; set; }
        public bool? ToEvaluate { get; set; }
    }
}
