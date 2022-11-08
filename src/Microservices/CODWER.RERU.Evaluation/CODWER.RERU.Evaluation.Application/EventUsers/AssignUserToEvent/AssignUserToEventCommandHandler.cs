using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
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
        private readonly AppDbContext _appDbContext;
        private readonly List<int> _addedUsersIds = new();

        public AssignUserToEventCommandHandler(AppDbContext appDbContext,
            IMapper mapper,
            INotificationService notificationService,
            IInternalNotificationService internalNotificationService
            )
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
        }

        public async Task<List<int>> Handle(AssignUserToEventCommand request, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            foreach (var userId in request.UserProfileId)
            {
                tasks.Add(Task.Run(() => AssignUsersToEvent(userId, request.EventId)));
            }

            await WaitTasks(Task.WhenAll(tasks));

            tasks.Clear();

            await UnassignUsersFromEvent(request.EventId);

            return _addedUsersIds;
        }

        private async Task WaitTasks(Task t)
        {
            try
            {
                t.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        private async Task AssignUsersToEvent(int userId, int eventId)
        {
            try
            {
                var exist = await ExistEventUser(userId, eventId);

                if (!exist)
                {
                    var result = await AssignUserToEvent(userId, eventId);

                    _addedUsersIds.Add(result.UserProfileId);

                    await _internalNotificationService
                        .AddNotification(result.UserProfileId, NotificationMessages.YouWereInvitedToEventAsCandidate);

                    await AddEmailNotification(result);
                }
                else
                {
                    _addedUsersIds.Add(userId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<EventUser> AssignUserToEvent(int userId, int eventId)
        {
            var newEventUser = new AddEventPersonDto()
            {
                UserProfileId = userId,
                EventId = eventId,
            };

            await using var db = _appDbContext.NewInstance();

            var result = _mapper.Map<EventUser>(newEventUser);

            await db.EventUsers.AddAsync(result);
            await db.SaveChangesAsync();

            return result;
        }

        private async Task UnassignUsersFromEvent(int eventId)
        {
            await using var db = _appDbContext.NewInstance();

            var userEventsToDelete = db.EventUsers
                .Where(eu => _addedUsersIds.All(p2 => p2 != eu.UserProfileId) && eu.EventId == eventId);

            if (userEventsToDelete.Any())
            {
                db.EventUsers.RemoveRange(userEventsToDelete);
                await db.SaveChangesAsync();
            }
        }

        private async Task AddEmailNotification(EventUser eventUser)
        {
            await using var db = _appDbContext.NewInstance();

            var item = await db.EventUsers
                .Include(x => x.UserProfile)
                .Include(x => x.Event)
                    .ThenInclude(x => x.EventLocations)
                .FirstOrDefaultAsync(x => x.Id == eventUser.Id);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la eveniment",
                To = item.UserProfile.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", item.UserProfile.FullName },
                    { "{email_message}", await GetTableContent(item) }
                }
            });
        }

        private async Task<string> GetTableContent(EventUser eventUser)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">sunteți invitat/ă la evenimentul ""{eventUser.Event.Name}"", în rol de candidat, care va avea loc în perioada 
                            {eventUser.Event.FromDate.ToString("dd/MM/yyyy HH:mm")}-{eventUser.Event.TillDate.ToString("dd/MM/yyyy HH:mm")}";

            content += eventUser.Event.EventLocations.Any() ? $@", locația {await GetLocationName(eventUser.Event)}.</p>" : $@".</p>";

            return content;
        }

        private async Task<bool> ExistEventUser(int userId, int eventId)
        {
            await using var db = _appDbContext.NewInstance();

            return db.EventUsers.Any(x =>
                x.UserProfileId == userId && x.EventId == eventId);
        }

        private async Task<string> GetLocationName(Event eventDb)
        {
            await using var db = _appDbContext.NewInstance();

            var locations = new List<EventLocation>();
            var list = new List<string>();

            locations = await db.EventLocations
                .Include(e => e.Location)
                .Where(e => e.EventId == eventDb.Id)
                .ToListAsync();

            list.AddRange(locations.Select(location => location.Location.Name + "-" + location.Location.Address));
            var combineString = string.Join(", ", list);

            return combineString;
        }
    }
}
