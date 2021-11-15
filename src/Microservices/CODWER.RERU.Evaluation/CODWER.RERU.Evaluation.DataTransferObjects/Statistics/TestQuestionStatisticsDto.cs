namespace CODWER.RERU.Evaluation.DataTransferObjects.Statistics
{
    public class TestQuestionStatisticDto
    {
        public int QuestionId { get; set; }
        public int TotalUsed { get; set; }
        public int CountByFilter { get; set; }
        public int Percent { get; set; }
        public int CountCorrect { get; set; }
        public int PercentCorrect { get; set; }
        public int CountNotCorrect { get; set; }
        public int PercentNotCorrect { get; set; }
        public int CountSkipped { get; set; }
        public int PercentSkipped { get; set; }
        public string Question { get; set; }
        public string CategoryName { get; set; }
    }
}
