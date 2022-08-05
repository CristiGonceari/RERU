using CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetUserSolicitedPositionDiagram
{
    public class GetUserSolicitedPositionDiagramQuery : IRequest<UserPositionDiagramDto>
    {
        public int PositionId { get; set; }
        public int? UserId { get; set; }
    }
}
