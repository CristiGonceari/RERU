using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;
using MediatR;

namespace CODWER.RERU.Core.Application.CandidatePositions.EditCandidatePosition
{
    public class EditCandidatePositionCommand : IRequest<Unit>
    {
        public AddEditCandidatePositionDto Data { get; set; }
    }
}
