using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions.DeleteEventVacantPosition
{
    public class DeleteEventVacantPositionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
