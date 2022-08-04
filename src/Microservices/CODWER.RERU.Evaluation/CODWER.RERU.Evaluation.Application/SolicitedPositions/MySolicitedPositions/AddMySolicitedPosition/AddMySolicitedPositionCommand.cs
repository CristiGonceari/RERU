using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.AddMySolicitedPosition
{
    public class AddMySolicitedPositionCommand : IRequest<AddSolicitedCandidatePositionResponseDto>
    {
        public AddEditSolicitedPositionDto Data { get; set; }
    }
}
