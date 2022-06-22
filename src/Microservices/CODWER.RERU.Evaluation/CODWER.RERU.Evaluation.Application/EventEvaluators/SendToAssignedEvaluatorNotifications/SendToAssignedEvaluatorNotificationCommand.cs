using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.SendToAssignedEvaluatorNotifications
{
    public class SendToAssignedEvaluatorNotificationCommand : IRequest<Unit>
    {
        public int UserProfileId { get; set; }
        public int EventId { get; set; }
    }
}
