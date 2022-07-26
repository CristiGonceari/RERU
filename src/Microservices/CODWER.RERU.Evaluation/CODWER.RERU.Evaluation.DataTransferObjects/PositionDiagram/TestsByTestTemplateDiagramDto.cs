using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram
{
    public class TestsByTestTemplateDiagramDto
    {
        public TestsByTestTemplateDiagramDto()
        {
            Tests = new List<TestResultDiagramDto>();
        }

        public int TestTemplateId { get; set; }
        public int EventId { get; set; }
        public List<TestResultDiagramDto> Tests { get; set; }
    }
}
