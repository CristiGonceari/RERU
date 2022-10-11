using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface ICandidatePositionNotificationService
    {
        Task AddCandidatePositionNotification(List<int> userProfileIds, int candidatePositionId);
    }
}
