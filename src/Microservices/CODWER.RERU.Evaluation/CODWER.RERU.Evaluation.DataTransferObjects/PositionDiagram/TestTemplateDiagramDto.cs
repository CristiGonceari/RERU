using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram
{
    public class TestTemplateDiagramDto
    {
        public int TestTemplateId { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public TestTemplateModeEnum Mode { get; set; }
    }
}
