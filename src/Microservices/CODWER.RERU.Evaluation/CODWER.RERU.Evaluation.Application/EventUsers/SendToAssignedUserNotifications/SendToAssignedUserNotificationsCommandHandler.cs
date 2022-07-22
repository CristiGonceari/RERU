using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventUsers.SendToAssignedUserNotifications
{
    public class SendToAssignedUserNotificationsCommandHandler : IRequestHandler<SendToAssignedUserNotificationsCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly INotificationService _notificationService;

        public SendToAssignedUserNotificationsCommandHandler(
            AppDbContext appDbContext, 
            IInternalNotificationService internalNotificationService, 
            INotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _internalNotificationService = internalNotificationService;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(SendToAssignedUserNotificationsCommand request, CancellationToken cancellationToken)
        {

            var eventUsers = _appDbContext.EventUsers
                .Include(x => x.Event)
                .FirstOrDefault(eu => eu.UserProfileId == request.UserProfileId && eu.EventId == request.EventId);

            await _internalNotificationService.AddNotification(eventUsers.UserProfileId, NotificationMessages.YouWereInvitedToEventAsCandidate);

            var email = await SendEmailNotification(eventUsers);

            return email;
        }

        private async Task<Unit> SendEmailNotification(EventUser eventUser)
        {
            var user = await _appDbContext.EventUsers
                .Include(eu => eu.UserProfile)
                .Include(eu => eu.Event)
                .FirstOrDefaultAsync(x => x.Id == eventUser.Id);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la eveniment",
                To = user.UserProfile.Email,
                HtmlTemplateAddress = "PdfTemplates/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.UserProfile.FullName },
                    { "{email_message}", await GetTableContent(eventUser.Event.Name) }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent(string eventName)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventName}"" în rol de candidat.</p>";

            return content;
        }
    }
}
