using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.GetCandidatePositionNotifications
{
    public class GetCandidatePositionNotificationsQuery : IRequest<List<int>>
    {
        public int CandidatePositionId { get; set; }
    }
}
