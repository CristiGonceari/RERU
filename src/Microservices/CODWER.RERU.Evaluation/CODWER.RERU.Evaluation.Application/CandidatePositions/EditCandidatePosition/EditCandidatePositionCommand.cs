using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.EditCandidatePosition
{
    public class EditCandidatePositionCommand : IRequest<Unit>
    {
        public AddEditCandidatePositionDto Data { get; set; }

    }
    
}
