using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.AddCandidatePosition
{
    public class AddCandidatePositionCommand : IRequest<int>
    {
        public AddEditCandidatePositionDto Data { get; set; }
    }
}
