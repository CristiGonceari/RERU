namespace CODWER.RERU.Evaluation.DataTransferObjects.TestQuestions
{
    public class TestOptionsDto
    {
        public int Id { get; set; }
        public int QuestionUnitId { get; set; }
        public string Answer { get; set; }
        public bool? IsSelected { get; set; }
        public string MediaFileId { get; set; }
    }
}
