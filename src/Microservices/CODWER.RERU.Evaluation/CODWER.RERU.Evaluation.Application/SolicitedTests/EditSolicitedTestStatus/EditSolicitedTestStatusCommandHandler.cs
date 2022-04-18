using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.EditSolicitedTestStatus
{
    public class EditSolicitedTestStatusCommandHandler : IRequestHandler<EditSolicitedTestStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly ILoggerService<EditSolicitedTestStatusCommandHandler> _loggerService;

        public EditSolicitedTestStatusCommandHandler(AppDbContext appDbContext, 
            INotificationService notificationService, 
            IInternalNotificationService internalNotificationService,
            ILoggerService<EditSolicitedTestStatusCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(EditSolicitedTestStatusCommand request, CancellationToken cancellationToken)
        {
            var solicitedTest = await _appDbContext.SolicitedTests
                .Include(s => s.UserProfile)
                .Include(s => s.TestTemplate)
                .FirstAsync(x => x.Id == request.Id);
            solicitedTest.SolicitedTestStatus = request.Status;

            await _appDbContext.SaveChangesAsync();

            if(solicitedTest.SolicitedTestStatus == SolicitedTestStatusEnum.Refused)
            {
                await _internalNotificationService.AddNotification(solicitedTest.UserProfileId, NotificationMessages.YourSolicitedTestWasRefused);

                await SendEmailNotification(solicitedTest);
            }

            await LogAction(solicitedTest);

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(SolicitedTest solicitedTest)
        {
            var user = await _appDbContext.SolicitedTests
                .Include(s => s.UserProfile)
                .Include(s => s.TestTemplate)
                .FirstOrDefaultAsync(x => x.UserProfileId == solicitedTest.UserProfileId);

            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            var template = await File.ReadAllTextAsync(path);

            template = template
                .Replace("{user_name}", user.UserProfile.FirstName + " " + user.UserProfile.LastName)
                .Replace("{email_message}", await GetTableContent(solicitedTest.TestTemplate.Name));

            var emailData = new EmailData
            {
                subject = "Invitație la eveniment",
                body = template,
                from = "Do Not Reply",
                to = user.UserProfile.Email
            };

            await _notificationService.Notify(emailData, NotificationType.Both);

            return Unit.Value;
        }

        private async Task<string> GetTableContent(string testName)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Testul ""{testName}"" solicitat de dumneavoastră a fost refuzat.</p>";

            return content;
        }

        private async Task LogAction(SolicitedTest item)
        {
            if(item.SolicitedTestStatus == SolicitedTestStatusEnum.Refused)
            {
                await _loggerService.Log(LogData.AsEvaluation($"Solicited test was refused", item));
            } 
            else if (item.SolicitedTestStatus == SolicitedTestStatusEnum.Approved)
            {
                await _loggerService.Log(LogData.AsEvaluation($"Solicited test was approved", item));
            }
        }
    }
}
