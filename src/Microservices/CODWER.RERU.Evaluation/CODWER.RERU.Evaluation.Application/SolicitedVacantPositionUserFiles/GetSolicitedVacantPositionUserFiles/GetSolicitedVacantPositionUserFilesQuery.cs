using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetSolicitedVacantPositionUserFiles
{
    public class GetSolicitedVacantPositionUserFilesQuery : IRequest<List<GetSolicitedVacantPositionDto>>
    {
        public int? UserId { get; set; }
        public int SolicitedVacantPositionId { get; set; }
        public int CandidatePositionId { get; set; }
    }
}
