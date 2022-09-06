using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.SendToAssignedEvaluatorNotifications
{
    public class SendToAssignedEvaluatorNotificationCommandHandler : IRequestHandler<SendToAssignedEvaluatorNotificationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly INotificationService _notificationService;

        public SendToAssignedEvaluatorNotificationCommandHandler(
            AppDbContext appDbContext,
            IInternalNotificationService internalNotificationService,
            INotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _internalNotificationService = internalNotificationService;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(SendToAssignedEvaluatorNotificationCommand request, CancellationToken cancellationToken)
        {
            var eventEvaluator = _appDbContext.EventEvaluators
                .Include(x => x.Event)
                .FirstOrDefault(ee => ee.EvaluatorId == request.UserProfileId && ee.EventId == request.EventId);

            await _internalNotificationService.AddNotification(eventEvaluator.EvaluatorId, NotificationMessages.YouWereInvitedToEventAsCandidate);

            var email = await SendEmailNotification(eventEvaluator);

            return email;
        }

        private async Task<Unit> SendEmailNotification(EventEvaluator eventEvaluator)
        {
            var user = await _appDbContext.EventEvaluators
                .Include(eu => eu.Evaluator)
                .Include(eu => eu.Event)
                .FirstOrDefaultAsync(x => x.Id == eventEvaluator.Id);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la eveniment",
                To = user.Evaluator.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.Evaluator.FullName },
                    { "{email_message}", await GetTableContent(eventEvaluator.Event.Name) }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent(string eventName)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">sunteți invitat/ă la evenimentul ""{eventName}"", în rol de evaluator.</p>";

            return content;
        }
    }
}
