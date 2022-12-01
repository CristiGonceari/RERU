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
        public bool canEvaluate { set; get; }
        // public bool canCounterSign { set; get; }
        // public bool canDownload { set; get; }
        // public bool canDelete { set; get; }
        // public bool canAccept { set; get; }
        // public bool canAcceptCounterSign { set; get; }
    }
}