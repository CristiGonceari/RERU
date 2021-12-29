using System.IO;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.AssignEvaluatorToEvent
{
    public class AssignEvaluatorToEventCommandHandler : IRequestHandler<AssignEvaluatorToEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public AssignEvaluatorToEventCommandHandler(AppDbContext appDbContext, IMapper mapper, INotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(AssignEvaluatorToEventCommand request, CancellationToken cancellationToken)
        {
            var eventEvaluator = _mapper.Map<EventEvaluator>(request.Data);

            await _appDbContext.EventEvaluators.AddAsync(eventEvaluator);
            await _appDbContext.SaveChangesAsync();

            await SendEmailNotification(eventEvaluator);

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(EventEvaluator eventEvaluator)
        {
            var user = await _appDbContext.EventEvaluators
                .Include(eu => eu.Evaluator)
                .Include(eu => eu.Event)
                .FirstOrDefaultAsync(x => x.Id == eventEvaluator.Id);

            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            var template = await File.ReadAllTextAsync(path);

            template = template
                .Replace("{user_name}", user.Evaluator.FirstName + " " + user.Evaluator.LastName)
                .Replace("{email_message}", await GetTableContent(eventEvaluator.Event.Name));

            var emailData = new EmailData()
            {
                subject = "Invitație la eveniment",
                body = template,
                from = "Do Not Reply",
                to = user.Evaluator.Email
            };

            await _notificationService.Notify(emailData, NotificationType.LocalNotification);

            return Unit.Value;
        }

        private async Task<string> GetTableContent(string eventName)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventName}"" în rol de evaluator.</p>";

            return content;
        }
    }
}
