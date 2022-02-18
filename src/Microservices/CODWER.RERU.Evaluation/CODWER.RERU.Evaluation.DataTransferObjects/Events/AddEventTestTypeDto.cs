namespace CODWER.RERU.Evaluation.DataTransferObjects.Events
{
    public class AddEventTestTypeDto
    {
        public int EventId { get; set; }
        public int TestTemplateId { get; set; }
        public int MaxAttempts { get; set; }
    }
}
