using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;
using MediatR;

namespace CODWER.RERU.Core.Application.CandidatePositions.GetCandidatePosition
{
    public class GetCandidatePositionQuery : IRequest<CandidatePositionDto>
    {
        public int Id { get; set; }
    }
}
