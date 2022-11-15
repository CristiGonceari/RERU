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
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.UserProfile.FullName },
                    { "{email_message}", GetTableContent(eventUser) }
                }
            });

            return Unit.Value;
        }

        private string GetTableContent(EventUser eventUser)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">sunteți invitat/ă la evenimentul ""{eventUser.Event.Name}"", în rol de candidat, care va avea loc în perioada 
                            {eventUser.Event.FromDate.ToString("dd/MM/yyyy HH:mm")}-{eventUser.Event.TillDate.ToString("dd/MM/yyyy HH:mm")}";

            content += eventUser.Event.EventLocations.Any() ? $@", locația {GetLocationName(eventUser.Event)}.</p>" : $@".</p>";

            return content;
        }

        private string GetLocationName(Event eventDb)
        {
            var locations = new List<EventLocation>();
            var list = new List<string>();

            locations = _appDbContext.EventLocations
                .Include(e => e.Location)
                .Where(e => e.EventId == eventDb.Id)
                .ToList();

            list.AddRange(locations.Select(location => location.Location.Name + "-" + location.Location.Address));
            var combineString = string.Join(", ", list);

            return combineString;
        }
    }
}
