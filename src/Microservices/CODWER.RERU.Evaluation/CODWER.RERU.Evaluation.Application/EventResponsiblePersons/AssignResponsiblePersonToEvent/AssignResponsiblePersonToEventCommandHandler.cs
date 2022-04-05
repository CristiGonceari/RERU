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

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.AssignResponsiblePersonToEvent
{
    public class AssignResponsiblePersonToEventCommandHandler : IRequestHandler<AssignResponsiblePersonToEventCommand, List<int>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly INotificationService _notificationService;

        public AssignResponsiblePersonToEventCommandHandler(AppDbContext appDbContext,
            IMapper mapper,
            INotificationService notificationService,
            IInternalNotificationService internalNotificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _internalNotificationService = internalNotificationService;
            _notificationService = notificationService;
        }

        public async Task<List<int>> Handle(AssignResponsiblePersonToEventCommand request, CancellationToken cancellationToken)
        {
            var eventUsersIds = new List<int>();

            var eventValues = await _appDbContext.EventResponsiblePersons.ToListAsync();

            foreach (var userId in request.UserProfileId)
            {
                var eventResponsiblePerson = eventValues.FirstOrDefault(l => l.UserProfileId == userId);

                if (eventResponsiblePerson == null)
                {
                    var newEventResponsiblePerson = new AddEventPersonDto()
                    {
                        UserProfileId = userId,
                        EventId = request.EventId,
                    };

                    var result = _mapper.Map<EventResponsiblePerson>(newEventResponsiblePerson);

                    await _appDbContext.EventResponsiblePersons.AddAsync(result);
                    await _appDbContext.SaveChangesAsync();

                    var eventName = await _appDbContext.EventResponsiblePersons
                        .Include(x => x.Event)
                        .FirstAsync(x => x.EventId == eventResponsiblePerson.EventId && x.UserProfileId == eventResponsiblePerson.UserProfileId);

                    eventUsersIds.Add(eventName.Id);

                    await _internalNotificationService.AddNotification(eventResponsiblePerson.UserProfileId, NotificationMessages.YouWereInvitedToEventAsResponsiblePerson);
                    await SendEmailNotification(result);
                }
                else
                {
                    eventUsersIds.Add(eventResponsiblePerson.Id);
                }

                eventValues = eventValues.Where(l => l.UserProfileId != userId).ToList();

            }

            if (eventValues.Count() > 0)
            {

                _appDbContext.EventResponsiblePersons.RemoveRange(eventValues);
                await _appDbContext.SaveChangesAsync();
            }

            return eventUsersIds;
        }

        private async Task<Unit> SendEmailNotification(EventResponsiblePerson eventResponsiblePerson)
        {
            var user = await _appDbContext.EventResponsiblePersons
                .Include(eu => eu.UserProfile)
                .Include(eu => eu.Event)
                .FirstOrDefaultAsync(x => x.Id == eventResponsiblePerson.Id);

            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            var template = await File.ReadAllTextAsync(path);

            template = template
                .Replace("{user_name}", user.UserProfile.FirstName + " " + user.UserProfile.LastName)
                .Replace("{email_message}", await GetTableContent(eventResponsiblePerson.Event.Name));

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
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventName}"" în rol de persoană responsabilă.</p>";

            return content;
        }
    }
}
