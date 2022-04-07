using MediatR;

namespace CODWER.RERU.Core.Application.CandidatePositions.DeleteCandidatePosition
{
    public class DeleteCandidatePositionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
