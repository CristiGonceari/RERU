using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.DeleteMySolicitedPosition
{
    public class DeleteMySolicitedPositionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
