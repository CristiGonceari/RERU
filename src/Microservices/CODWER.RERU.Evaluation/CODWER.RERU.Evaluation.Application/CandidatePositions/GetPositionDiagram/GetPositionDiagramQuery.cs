using CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionDiagram
{
    public class GetPositionDiagramQuery : IRequest<PositionDiagramDto>
    {
        public int PositionId { get; set; }
    }
}
