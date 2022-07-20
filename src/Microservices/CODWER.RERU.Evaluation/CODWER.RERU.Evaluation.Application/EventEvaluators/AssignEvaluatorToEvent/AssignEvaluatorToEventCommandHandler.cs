using System.IO;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using System.Linq;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.AssignEvaluatorToEvent
{
    public class AssignEvaluatorToEventCommandHandler : IRequestHandler<AssignEvaluatorToEventCommand, List<int>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;

        public AssignEvaluatorToEventCommandHandler(AppDbContext appDbContext,
            IMapper mapper,
            INotificationService notificationService,
            IInternalNotificationService internalNotificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
        }

        public async Task<List<int>> Handle(AssignEvaluatorToEventCommand request, CancellationToken cancellationToken)
        {

            var eventEvaluatorIds = new List<int>();

            var eventValues = await _appDbContext.EventEvaluators.Where(ee => ee.EventId == request.EventId).ToListAsync();

            foreach (var evaluatorId in request.EvaluatorId)
            {
                var eventEvaluator = eventValues.FirstOrDefault(l => l.EvaluatorId == evaluatorId);

                if (eventEvaluator == null)
                {
                    var newEventEvaluator = new AddEventEvaluatorDto()
                    {
                        EvaluatorId = evaluatorId,
                        EventId = request.EventId,
                        ShowUserName = request.ShowUserName
                    };

                    var result = _mapper.Map<EventEvaluator>(newEventEvaluator);

                    await _appDbContext.EventEvaluators.AddAsync(result);
                    await _appDbContext.SaveChangesAsync();

                    eventEvaluatorIds.Add(evaluatorId);

                    await _internalNotificationService.AddNotification(result.EvaluatorId, NotificationMessages.YouWereInvitedToEventAsCandidate);

                    await AddEmailNotification(result);
                }
                else
                {
                    eventEvaluatorIds.Add(eventEvaluator.EvaluatorId);
                }

                eventValues = eventValues.Where(l => l.EvaluatorId != evaluatorId).ToList();

            }

            if (eventValues.Any())
            {

                _appDbContext.EventEvaluators.RemoveRange(eventValues);
            }

            await _appDbContext.SaveChangesAsync();

            return eventEvaluatorIds;
        }

        private async Task AddEmailNotification(EventEvaluator eventEvaluator)
        {
            var item = new EmailNotification
            {
                ItemId = eventEvaluator.Id,
                EmailType = EmailType.AssignEvaluatorToEvent,
                IsSend = false
            };

            await _appDbContext.EmailNotifications.AddAsync(item);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
