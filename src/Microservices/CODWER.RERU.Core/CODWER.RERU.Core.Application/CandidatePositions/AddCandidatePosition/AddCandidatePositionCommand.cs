using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;
using MediatR;

namespace CODWER.RERU.Core.Application.CandidatePositions.AddCandidatePosition
{
    public class AddCandidatePositionCommand : IRequest<int>
    {
        public AddEditCandidatePositionDto Data { get; set; }
    }
}
