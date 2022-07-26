using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram
{
    public class EventDiagramDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public List<TestTemplateDiagramDto> TestTemplates { get; set; }
    }
}
