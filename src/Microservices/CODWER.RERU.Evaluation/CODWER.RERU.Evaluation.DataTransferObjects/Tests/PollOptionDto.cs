namespace CODWER.RERU.Evaluation.DataTransferObjects.Tests
{
    public class PollOptionDto
    {
        public int OptionId { get; set; }
        public string Answer { get; set; }
        public int? VotedCount { get; set; }
        public double VotedPercent { get; set; }
    }
}
