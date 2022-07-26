using System;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram
{
    public class TestResultDiagramDto
    {
        public int TestId { get; set; }
        public string Result { get; set; }
        public DateTime PassDate { get; set; }
        public TestStatusEnum Status { get; set; }
    }
}
