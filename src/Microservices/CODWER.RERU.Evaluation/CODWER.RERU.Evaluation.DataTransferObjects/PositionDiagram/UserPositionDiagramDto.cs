using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram
{
    public class UserPositionDiagramDto
    {
        public List<EventDiagramDto> EventsDiagram { get; set; }
        public UserDiagramDto UserDiagram { get; set; }
    }
}
