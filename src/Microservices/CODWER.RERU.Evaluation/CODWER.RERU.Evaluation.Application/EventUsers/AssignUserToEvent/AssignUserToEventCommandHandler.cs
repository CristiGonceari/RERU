using System.IO;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.Services;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.EventUsers.AssignUserToEvent
{
    public class AssignUserToEventCommandHandler : IRequestHandler<AssignUserToEventCommand, List<int>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;

        public AssignUserToEventCommandHandler(AppDbContext appDbContext,
            IMapper mapper,
            INotificationService notificationService,
            IInternalNotificationService internalNotificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
        }

        public async Task<List<int>> Handle(AssignUserToEventCommand request, CancellationToken cancellationToken)
        {
            var eventUsersIds = new List<int>();

            var eventValues = await _appDbContext.EventUsers.ToListAsync();

            foreach (var userId in request.UserProfileId)
            {
                var eventUser = eventValues.FirstOrDefault(l => l.UserProfileId == userId);

                if (eventUser == null)
                {
                    var newEventUser = new AddEventPersonDto()
                    {
                        UserProfileId = userId,
                        EventId = request.EventId,
                    };

                    var result = _mapper.Map<EventUser>(newEventUser);

                    await _appDbContext.EventUsers.AddAsync(result);
                    await _appDbContext.SaveChangesAsync();

                    var eventName = await _appDbContext.EventUsers
                       .Include(x => x.Event)
                       .FirstAsync(x => x.EventId == eventUser.EventId && x.UserProfileId == eventUser.UserProfileId);

                    eventUsersIds.Add(eventName.Id);

                    await _internalNotificationService.AddNotification(eventUser.UserProfileId, NotificationMessages.YouWereInvitedToEventAsCandidate);

                    await SendEmailNotification(result);
                }
                else
                {
                    eventUsersIds.Add(eventUser.Id);
                }

                eventValues = eventValues.Where(l => l.UserProfileId != userId).ToList();

            }

            if (eventValues.Count() > 0)
            {

                _appDbContext.EventUsers.RemoveRange(eventValues);
                await _appDbContext.SaveChangesAsync();
            }

            return eventUsersIds;
        }

        private async Task<Unit> SendEmailNotification(EventUser eventUser)
        {
            var user = await _appDbContext.EventUsers
                .Include(eu => eu.UserProfile)
                .Include(eu => eu.Event)
                .FirstOrDefaultAsync(x => x.Id == eventUser.Id);

            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            var template = await File.ReadAllTextAsync(path);

            template = template
                .Replace("{user_name}", user.UserProfile.FirstName + " " + user.UserProfile.LastName)
                .Replace("{email_message}", await GetTableContent(eventUser.Event.Name));

            var emailData = new EmailData()
            {
                subject = "Invitație la eveniment",
                body = template,
                from = "Do Not Reply",
                to = user.UserProfile.Email
            };

            await _notificationService.Notify(emailData, NotificationType.Both);

            return Unit.Value;
        }

        private async Task<string> GetTableContent(string eventName)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventName}"" în rol de candidat.</p>";

            return content;
        }
    }
}
