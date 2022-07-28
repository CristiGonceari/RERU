using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.GetMySolicitedPosition
{
    public class GetMySolicitedPositionQuery : IRequest<SolicitedCandidatePositionDto>
    {
        public int Id { get; set; }
    }
}
