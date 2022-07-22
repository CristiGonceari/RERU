using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;

namespace CODWER.RERU.Evaluation.Application.EventUsers.AssignUserToEvent
{
    public class AssignUserToEventCommandHandler : IRequestHandler<AssignUserToEventCommand, List<int>>
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly IConfiguration _configuration;

        public AssignUserToEventCommandHandler(AppDbContext appDbContext,
            IMapper mapper,
            INotificationService notificationService,
            IInternalNotificationService internalNotificationService,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
            _configuration = configuration;
        }

        public async Task<List<int>> Handle(AssignUserToEventCommand request, CancellationToken cancellationToken)
        {
            var eventUsersIds = new List<int>();

            await using var db = AppDbContext.NewInstance(_configuration);
            var eventValues = await db.EventUsers.ToListAsync();

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

                    await db.EventUsers.AddAsync(result);
                    await db.SaveChangesAsync();

                    var eventName = await db.EventUsers
                       .Include(x => x.Event)
                       .Include(x => x.UserProfile)
                       .FirstAsync(x => x.EventId == request.EventId && x.UserProfileId == userId);

                    eventUsersIds.Add(eventName.Id);

                    await _internalNotificationService.AddNotification(result.UserProfileId, NotificationMessages.YouWereInvitedToEventAsCandidate);

                    await AddEmailNotification(result);
                }
                else
                {
                    eventUsersIds.Add(eventUser.Id);
                }

                eventValues = eventValues.Where(l => l.UserProfileId != userId).ToList();
            }

            if (eventValues.Any())
            {
                db.EventUsers.RemoveRange(eventValues);
                await db.SaveChangesAsync();
            }

            return eventUsersIds;
        }

        private async Task AddEmailNotification(EventUser eventUser)
        {
            await using var db = AppDbContext.NewInstance(_configuration);

            var item = await db.EventUsers
                .Include(x => x.UserProfile)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == eventUser.Id);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la eveniment",
                To = item.UserProfile.Email,
                HtmlTemplateAddress = "PdfTemplates/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", item.UserProfile.FullName },
                    { "{email_message}", GetTableContent(item) }
                }
            });
        }

        private string GetTableContent(EventUser eventUser)
            => $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventUser.Event.Name}"" în rol de candidat.</p>";
    }
}
