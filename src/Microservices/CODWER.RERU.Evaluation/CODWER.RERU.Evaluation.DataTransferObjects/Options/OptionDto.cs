namespace CODWER.RERU.Evaluation.DataTransferObjects.Options
{
    public class OptionDto
    {
        public int Id { get; set; }
        public int QuestionUnitId { get; set; }
        public string Answer { get; set; }
        public bool? IsCorrect { get; set; }
        public string? MediaFileId { get; set; }
    }
}
