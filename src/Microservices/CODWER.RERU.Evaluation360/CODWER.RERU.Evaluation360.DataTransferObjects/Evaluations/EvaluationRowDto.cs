namespace CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations
{
    public class EvaluationRowDto
    {
        public int Id { set; get; }
        public string EvaluatedName { set; get; }
        public string EvaluatorName { set; get; }
        public string CounterSignerName { set; get; }
        public int Type { set; get; }
        public decimal Points { set; get; }
        public int Status { set; get; }
    }
}