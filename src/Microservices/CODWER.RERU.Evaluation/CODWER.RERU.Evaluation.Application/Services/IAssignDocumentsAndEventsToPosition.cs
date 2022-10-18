using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using RERU.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IAssignDocumentsAndEventsToPosition
    {
        Task AssignRequiredDocumentsToPosition(List<AssignTagsValuesDto> requiredDocuments, CandidatePosition position);
        Task AssignEventToPosition(List<int> eventIds, CandidatePosition position);
    }
}
