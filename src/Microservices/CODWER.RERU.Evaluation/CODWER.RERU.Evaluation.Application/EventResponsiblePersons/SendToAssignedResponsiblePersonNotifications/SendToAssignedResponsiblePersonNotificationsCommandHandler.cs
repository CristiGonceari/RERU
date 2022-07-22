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

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.SendToAssignedResponsiblePersonNotifications
{
    public class SendToAssignedResponsiblePersonNotificationsCommandHandler : IRequestHandler<SendToAssignedResponsiblePersonNotificationsCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly INotificationService _notificationService;

        public SendToAssignedResponsiblePersonNotificationsCommandHandler(AppDbContext appDbContext,
            INotificationService notificationService,
            IInternalNotificationService internalNotificationService)
        {
            _appDbContext = appDbContext;
            _internalNotificationService = internalNotificationService;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(SendToAssignedResponsiblePersonNotificationsCommand request, CancellationToken cancellationToken)
        {
            var eventResponsiblePerson = _appDbContext.EventResponsiblePersons
                .Include(x => x.Event)
                .FirstOrDefault(erp => erp.UserProfileId == request.UserProfileId && erp.EventId == request.EventId);

            await _internalNotificationService.AddNotification(eventResponsiblePerson.UserProfileId, NotificationMessages.YouWereInvitedToEventAsCandidate);

            var email = await SendEmailNotification(eventResponsiblePerson);

            return  email;
        }

        private async Task<Unit> SendEmailNotification(EventResponsiblePerson eventResponsiblePerson)
        {
            var user = await _appDbContext.EventResponsiblePersons
                .Include(eu => eu.UserProfile)
                .Include(eu => eu.Event)
                .FirstOrDefaultAsync(x => x.Id == eventResponsiblePerson.Id);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la eveniment",
                To = user.UserProfile.Email,
                HtmlTemplateAddress = "PdfTemplates/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.UserProfile.FullName },
                    { "{email_message}", await GetTableContent(eventResponsiblePerson.Event.Name) }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent(string eventName)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventName}"" în rol de persoană responsabilă.</p>";

            return content;
        }
    }
}
