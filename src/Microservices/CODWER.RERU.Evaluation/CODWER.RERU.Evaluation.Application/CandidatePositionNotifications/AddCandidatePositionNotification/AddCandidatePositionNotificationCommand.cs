using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositionNotifications;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.AddCandidatePositionNotification
{
    public class AddCandidatePositionNotificationCommand : IRequest<int>
    {
        public CandidatePositionNotificationDto Data { get; set; }
    }
}
