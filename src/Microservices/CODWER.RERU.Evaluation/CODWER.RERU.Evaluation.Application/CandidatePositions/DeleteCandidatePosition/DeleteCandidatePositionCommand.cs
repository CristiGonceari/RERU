using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.DeleteCandidatePosition
{
    public class DeleteCandidatePositionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
