using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Events
{
    public class TestTemplatesByEventDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public List<SelectTestTemplateValueDto> TestTemplates { get; set; }
    }
}
