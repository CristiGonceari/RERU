using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePosition
{
    public class GetCandidatePositionQuery : IRequest<CandidatePositionDto>
    {
        public int Id { get; set; }
    }
}
