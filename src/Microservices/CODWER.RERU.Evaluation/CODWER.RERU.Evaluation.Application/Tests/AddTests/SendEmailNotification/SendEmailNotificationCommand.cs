using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests.SendEmailNotification
{
    public class SendEmailNotificationCommand : IRequest<Unit>
    {
        public List<int> TestIds { get; set; }
    }
}
 