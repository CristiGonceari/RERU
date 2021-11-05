namespace CODWER.RERU.Evaluation.DataTransferObjects.Events
{
    public class SelectEventValueDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public bool IsEventEvaluator { get; set; }
    }
}
