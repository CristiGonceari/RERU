using CODWER.RERU.Evaluation.DataTransferObjects.EventVacantPositions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions.AddEditEventVacantPosition
{
    public class AddEditEventVacantPositionCommand : IRequest<int>
    {
        public AddEditEventVacantPositionDto Data { get; set; }
    }
}
