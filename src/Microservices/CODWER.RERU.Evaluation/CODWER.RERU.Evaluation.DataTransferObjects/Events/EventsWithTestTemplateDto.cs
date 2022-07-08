using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Events
{
    public class EventsWithTestTemplateDto
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public List<string> TestTemplateNames { get; set; }
    }
}
