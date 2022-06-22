using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventUsers.SendToAssignedUserNotifications
{
    public class SendToAssignedUserNotificationsCommand : IRequest<Unit>
    {
        public int UserProfileId { get; set; }
        public int EventId { get; set; }
    }
}
