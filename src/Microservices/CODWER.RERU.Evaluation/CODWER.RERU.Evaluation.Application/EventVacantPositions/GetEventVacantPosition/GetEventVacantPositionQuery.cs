using CODWER.RERU.Evaluation.DataTransferObjects.EventVacantPositions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions.GetEventVacantPosition
{
    public class GetEventVacantPositionQuery : IRequest<EventVacantPositionDto>
    {
        public int Id { get; set; }
    }
}
