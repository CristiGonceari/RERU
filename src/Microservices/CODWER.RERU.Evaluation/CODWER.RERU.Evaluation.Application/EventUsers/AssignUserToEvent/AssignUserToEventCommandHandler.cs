using System.IO;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.Services;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using System.Linq;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

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

                    eventUsersIds.Add(userId);
                }
                else
                {
                    eventUsersIds.Add(eventUser.UserProfileId);
                }

                eventValues = eventValues.Where(l => l.UserProfileId != userId).ToList();

            }

            if (eventValues.Count() > 0)
            {

                _appDbContext.EventUsers.RemoveRange(eventValues);
               
            }

            await _appDbContext.SaveChangesAsync();

            return eventUsersIds;
        }
    }
}
