using MediatR;

namespace CODWER.RERU.Evaluation.Application.InternalNotifications.MarkNotificationAsSeen
{
    public class MarkNotificationAsSeenCommand : IRequest<Unit>
    {
        public int NotificationId { get; set; }
    }
}
