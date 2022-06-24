using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using RERU.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IAssignDocumentsToPosition
    {
        Task AssignRequiredDocumentsToPosition(List<AssignRequiredDocumentsDto> requiredDocuments, CandidatePosition position);
        Task<int> AddRequiredDocumentCommand(AssignRequiredDocumentsDto document);
        Task AddRequiredDocumentPosition(int requiredDocumentId, int positionId);
    }
}
