using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram
{
    public class PositionDiagramDto
    {
        public List<EventDiagramDto> EventsDiagram { get; set; }
        public List<UserDiagramDto> UsersDiagram { get; set; }
    }
}
