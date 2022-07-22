using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.AddMySolicitedPosition
{
    public class AddMySolicitedPositionCommand : IRequest<AddSolicitedCandidatePositionResponseDto>
    {
        public AddEditSolicitedPositionDto Data { get; set; }
    }
}
