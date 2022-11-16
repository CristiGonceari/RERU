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

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.AssignEvaluatorToEvent
{
    public class AssignEvaluatorToEventCommandHandler : IRequestHandler<AssignEvaluatorToEventCommand, List<int>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly ILoggerService<AssignEvaluatorToEventCommand> _loggerService;

        public AssignEvaluatorToEventCommandHandler(AppDbContext appDbContext,
            IMapper mapper,
            INotificationService notificationService,
            IInternalNotificationService internalNotificationService, 
            ILoggerService<AssignEvaluatorToEventCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
            _loggerService = loggerService;
        }

        public async Task<List<int>> Handle(AssignEvaluatorToEventCommand request, CancellationToken cancellationToken)
        {
            var eventEvaluatorIds = new List<int>();

            var eventValues = await _appDbContext.EventEvaluators
                .Include(x => x.Event)
                .Include(x => x.Evaluator)
                .Where(ee => ee.EventId == request.EventId).ToListAsync();

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

                    await _internalNotificationService.AddNotification(result.EvaluatorId, NotificationMessages.YouWereInvitedToEventAsEvaluator);

                    await LogAction(result);

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
                await LogAction(eventValues);

                _appDbContext.EventEvaluators.RemoveRange(eventValues);
            }

            await _appDbContext.SaveChangesAsync();

            return eventEvaluatorIds;
        }

        private async Task AddEmailNotification(EventEvaluator eventEvaluator)
        {
            var item = await GetEventEvaluator(eventEvaluator.Id);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Notificare de eveniment",
                To = item.Evaluator.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", item.Evaluator.FullName },
                    { "{email_message}", GetTableContent(item) }
                }
            });
        }

        private async Task LogAction(EventEvaluator evEvaluator)
        {
            var eventEvaluator = await GetEventEvaluator(evEvaluator.Id);

            await _loggerService.Log(LogData.AsEvaluation($"{eventEvaluator.Evaluator.FullName} a fost adăgat în rol de evaluator la evenimentul {eventEvaluator.Event.Name}"));
        }

        private async Task LogAction(List<EventEvaluator> eventEvaluators)
        {
            foreach (var item in eventEvaluators)
            {
                await _loggerService.Log(LogData.AsEvaluation($"{item.Evaluator.FullName} a fost șters din evenimentul {item.Event.Name} ca rol de evaluator"));
            }
        }

        private async Task<EventEvaluator> GetEventEvaluator(int id) =>
            await _appDbContext.EventEvaluators
                .Include(x => x.Event)
                .Include(x => x.Evaluator)
                .FirstOrDefaultAsync(x => x.Id == id);

        private string GetTableContent(EventEvaluator eventEvaluator)
            => $@"<p style=""font-size: 22px; font-weight: 300;"">sunteți invitat/ă la evenimentul ""{eventEvaluator.Event.Name}"", în rol de evaluator.</p>";
    }
}
