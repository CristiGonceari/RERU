using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

            if (eventValues.Count > 0)
            {

                db.EventUsers.RemoveRange(eventValues);
                await db.SaveChangesAsync();
            }

            return eventUsersIds;
        }

        private async Task AddEmailNotification(EventUser eventUser)
        {
            await using var db = AppDbContext.NewInstance(_configuration);

            var item = new EmailNotification
            {
                ItemId = eventUser.Id,
                EmailType = EmailType.AssignUserToEvent,
                IsSend = false
            };

            await db.EmailNotifications.AddAsync(item);
            await db.SaveChangesAsync();
        }
    }
}
