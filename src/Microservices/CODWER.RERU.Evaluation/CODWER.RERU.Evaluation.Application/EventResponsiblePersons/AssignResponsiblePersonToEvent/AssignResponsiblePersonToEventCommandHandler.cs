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

            var eventValues = await _appDbContext.EventResponsiblePersons.Where(erp => erp.EventId == request.EventId).ToListAsync();

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

                    eventUsersIds.Add(userId);

                }
                else
                {
                    eventUsersIds.Add(eventResponsiblePerson.UserProfileId);
                }

                eventValues = eventValues.Where(l => l.UserProfileId != userId).ToList();

            }

            if (eventValues.Count() > 0)
            {

                _appDbContext.EventResponsiblePersons.RemoveRange(eventValues);
            }

            await _appDbContext.SaveChangesAsync();

            return eventUsersIds;
        }
    }
}
