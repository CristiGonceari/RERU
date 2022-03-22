namespace CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests
{
    public class VerificationTestOptionsDto
    {
        public int Id { get; set; }
        public int QuestionUnitId { get; set; }
        public string Answer { get; set; }
        public bool? IsSelected { get; set; }
        public bool IsCorrect { get; set; }
        public string OptionMediaFileId { get; set; }
        public string Points { get; set; }
        public int Percentages { get; set; }
    }
}
