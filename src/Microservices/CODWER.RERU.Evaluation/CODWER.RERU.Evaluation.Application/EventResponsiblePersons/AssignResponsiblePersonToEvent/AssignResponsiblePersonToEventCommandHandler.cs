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

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.AssignResponsiblePersonToEvent
{
    public class AssignResponsiblePersonToEventCommandHandler : IRequestHandler<AssignResponsiblePersonToEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public AssignResponsiblePersonToEventCommandHandler(AppDbContext appDbContext, IMapper mapper, INotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(AssignResponsiblePersonToEventCommand request, CancellationToken cancellationToken)
        {
            var eventResponsiblePerson = _mapper.Map<EventResponsiblePerson>(request.Data);

            await _appDbContext.EventResponsiblePersons.AddAsync(eventResponsiblePerson);
            await _appDbContext.SaveChangesAsync();

            await SendEmailNotification(eventResponsiblePerson);

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(EventResponsiblePerson eventResponsiblePerson)
        {
            var user = await _appDbContext.EventResponsiblePersons
                .Include(eu => eu.UserProfile)
                .Include(eu => eu.Event)
                .FirstOrDefaultAsync(x => x.Id == eventResponsiblePerson.Id);

            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            var template = await File.ReadAllTextAsync(path);

            template = template
                .Replace("{user_name}", user.UserProfile.FirstName + " " + user.UserProfile.LastName)
                .Replace("{email_message}", await GetTableContent(eventResponsiblePerson.Event.Name));

            var emailData = new EmailData()
            {
                subject = "Invitație la eveniment",
                body = template,
                from = "Do Not Reply",
                to = user.UserProfile.Email
            };

            await _notificationService.Notify(emailData, NotificationType.LocalNotification);

            return Unit.Value;
        }

        private async Task<string> GetTableContent(string eventName)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventName}"" în rol de persoană responsabilă.</p>";

            return content;
        }
    }
}
