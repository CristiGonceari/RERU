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
            foreach (var userId in request.UserProfileId)
            {
                var exist = ExistEventUser(userId, request.EventId);

                if (!exist)
                {
                    var result = await AssignUserToEvent(userId, request.EventId);

                    _addedUsersIds.Add(result.UserProfileId);

                    await _internalNotificationService.AddNotification(result.UserProfileId, NotificationMessages.YouWereInvitedToEventAsCandidate);

                    await AddEmailNotification(result);
                }
                else
                {
                    _addedUsersIds.Add(userId);
                }
            }

            await UnassignUsersFromEvent(request.EventId);

            return _addedUsersIds;
        }

        private async Task<EventUser> AssignUserToEvent(int userId, int eventId)
        {
            var newEventUser = new AddEventPersonDto()
            {
                UserProfileId = userId,
                EventId = eventId,
            };

            var result = _mapper.Map<EventUser>(newEventUser);

            await _appDbContext.EventUsers.AddAsync(result);
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        private async Task UnassignUsersFromEvent(int eventId)
        {
            var userEventsToDelete = _appDbContext.EventUsers
                .Where(eu => _addedUsersIds.All(p2 => p2 != eu.UserProfileId) && eu.EventId == eventId);

            if (userEventsToDelete.Any())
            {
                _appDbContext.EventUsers.RemoveRange(userEventsToDelete);
                await _appDbContext.SaveChangesAsync();
            }
        }

        private async Task AddEmailNotification(EventUser eventUser)
        {
            var item = await _appDbContext.EventUsers
                .Include(x => x.UserProfile)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == eventUser.Id);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la eveniment",
                To = item.UserProfile.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", item.UserProfile.FullName },
                    { "{email_message}", GetTableContent(item) }
                }
            });
        }

        private string GetTableContent(EventUser eventUser)
            => $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventUser.Event.Name}"" în rol de candidat.</p>";

        private bool ExistEventUser(int userId, int eventId) => 
            _appDbContext.EventUsers.Any(x =>
            x.UserProfileId == userId && x.EventId == eventId);
    }
}
