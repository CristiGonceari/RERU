using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
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

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.AssignResponsiblePersonToEvent
{
    public class AssignResponsiblePersonToEventCommandHandler : IRequestHandler<AssignResponsiblePersonToEventCommand, List<int>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly INotificationService _notificationService;
        private readonly ILoggerService<AssignResponsiblePersonToEventCommand> _loggerService;

        public AssignResponsiblePersonToEventCommandHandler(AppDbContext appDbContext,
            IMapper mapper,
            INotificationService notificationService,
            IInternalNotificationService internalNotificationService, 
            ILoggerService<AssignResponsiblePersonToEventCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _internalNotificationService = internalNotificationService;
            _loggerService = loggerService;
            _notificationService = notificationService;
        }

        public async Task<List<int>> Handle(AssignResponsiblePersonToEventCommand request, CancellationToken cancellationToken)
        {
            var eventUsersIds = new List<int>();

            var eventValues = await _appDbContext.EventResponsiblePersons
                .Include(x => x.UserProfile)
                .Include(x => x.Event)
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

                    await _internalNotificationService.AddNotification(result.UserProfileId, NotificationMessages.YouWereInvitedToEventAsResponsiblePerson);

                    await LogAction(result);

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
                await LogAction(eventValues);
            }

            await _appDbContext.SaveChangesAsync();

            return eventUsersIds;
        }

        private async Task AddEmailNotification(EventResponsiblePerson eventResponsiblePerson)
        {
            var item = await GetEventResponsiblePerson(eventResponsiblePerson.Id);

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

        private async Task LogAction(EventResponsiblePerson eventResponsiblePerson)
        {
            var item = await GetEventResponsiblePerson(eventResponsiblePerson.Id);

            await _loggerService.Log(LogData.AsEvaluation($"{item.UserProfile.FullName} a fost adăgat în rol de persoană responsabilă la evenimentul {item.Event.Name}"));
        }

        private async Task LogAction(List<EventResponsiblePerson> eventResponsiblePersons)
        {
            foreach (var item in eventResponsiblePersons)
            {
                await _loggerService.Log(LogData.AsEvaluation($"{item.UserProfile.FullName} a fost ștersă din evenimentul {item.Event.Name} in rol de persoană responsabilă"));
            }
        }

        private async Task<EventResponsiblePerson> GetEventResponsiblePerson(int id) => 
            await _appDbContext.EventResponsiblePersons
                .Include(x => x.Event)
                .Include(x => x.UserProfile)
                .FirstOrDefaultAsync(x => x.Id == id);
        

        private string GetTableContent(EventResponsiblePerson eventResponsiblePerson)
         => $@"<p style=""font-size: 22px; font-weight: 300;"">sunteți invitat/ă la evenimentul ""{eventResponsiblePerson.Event.Name}"", în rol de persoană responsabilă.</p>";
    }
}
