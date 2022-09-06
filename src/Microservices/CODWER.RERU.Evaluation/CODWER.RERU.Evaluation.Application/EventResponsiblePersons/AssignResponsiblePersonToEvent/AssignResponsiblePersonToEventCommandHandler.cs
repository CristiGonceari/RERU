using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;

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

            var eventValues = await _appDbContext.EventResponsiblePersons
                .Where(erp => erp.EventId == request.EventId)
                .ToListAsync();

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

                    eventUsersIds.Add(userId);

                    await _internalNotificationService.AddNotification(result.UserProfileId, NotificationMessages.YouWereInvitedToEventAsCandidate);

                    await AddEmailNotification(result);
                }
                else
                {
                    eventUsersIds.Add(eventResponsiblePerson.UserProfileId);
                }
                eventValues = eventValues.Where(l => l.UserProfileId != userId).ToList();
            }

            if (eventValues.Any())
            {
                _appDbContext.EventResponsiblePersons.RemoveRange(eventValues);
            }

            await _appDbContext.SaveChangesAsync();

            return eventUsersIds;
        }

        private async Task AddEmailNotification(EventResponsiblePerson eventResponsiblePerson)
        {
            var item = await _appDbContext.EventResponsiblePersons
                .Include(eu => eu.UserProfile)
                .Include(eu => eu.Event)
                .FirstOrDefaultAsync(x => x.Id == eventResponsiblePerson.Id);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Notificare de test",
                To = item.UserProfile.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", item.UserProfile.FullName },
                    { "{email_message}", GetTableContent(item) }
                }
            });
        }

        private string GetTableContent(EventResponsiblePerson eventResponsiblePerson)
         => $@"<p style=""font-size: 22px; font-weight: 300;"">sunteți invitat/ă la evenimentul ""{eventResponsiblePerson.Event.Name}"", în rol de persoană responsabilă.</p>";
    }
}
