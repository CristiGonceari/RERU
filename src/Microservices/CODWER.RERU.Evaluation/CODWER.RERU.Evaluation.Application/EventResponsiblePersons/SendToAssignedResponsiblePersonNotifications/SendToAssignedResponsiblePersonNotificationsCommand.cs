using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.SendToAssignedResponsiblePersonNotifications
{
    public class SendToAssignedResponsiblePersonNotificationsCommand : IRequest<Unit>
    {
        public int UserProfileId { get; set; }
    }
}
