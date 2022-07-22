using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.EditSolicitedPositionStatus
{
    public class EditSolicitedPositionStatusCommandHandler : IRequestHandler<EditSolicitedPositionStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly ILoggerService<EditSolicitedPositionStatusCommandHandler> _loggerService;

        public EditSolicitedPositionStatusCommandHandler(AppDbContext appDbContext, 
            IInternalNotificationService internalNotificationService,
            ILoggerService<EditSolicitedPositionStatusCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _internalNotificationService = internalNotificationService;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(EditSolicitedPositionStatusCommand request, CancellationToken cancellationToken)
        {
            var solicitedTest = await _appDbContext.SolicitedVacantPositions
                .Include(s => s.UserProfile)
                .FirstAsync(x => x.Id == request.Id);
            
            solicitedTest.SolicitedPositionStatus = request.Status;

            await _appDbContext.SaveChangesAsync();

            if(solicitedTest.SolicitedPositionStatus == SolicitedPositionStatusEnum.Refused)
            {
                await _internalNotificationService.AddNotification(solicitedTest.UserProfileId, NotificationMessages.YourSolicitedTestWasRefused);

                //await SendEmailNotification(solicitedTest);
            }

            await LogAction(solicitedTest);

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(SolicitedVacantPosition solicitedVacantPosition)
        {
            var user = await _appDbContext.SolicitedVacantPositions
                .Include(s => s.UserProfile)
                //.Include(s => s.TestTemplate)
                .FirstOrDefaultAsync(x => x.UserProfileId == solicitedVacantPosition.UserProfileId);

            //var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            //var template = await File.ReadAllTextAsync(path);

            //template = template
            //    .Replace("{user_name}", user.UserProfile.FirstName + " " + user.UserProfile.LastName);
            //    //.Replace("{email_message}", await GetTableContent(solicitedVacantPosition.TestTemplate.Name));

            //var emailData = new EmailData
            //{
            //    subject = "Invitație la eveniment",
            //    body = template,
            //    from = "Do Not Reply",
            //    to = user.UserProfile.Email
            //};

            //await _notificationService.Notify(emailData, NotificationType.Both);

            return Unit.Value;
        }

        private async Task<string> GetTableContent()
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Solicitarea dumneavoastră a fost refuzată.</p>";

            return content;
        }

        private async Task LogAction(SolicitedVacantPosition item)
        {
            if(item.SolicitedPositionStatus == SolicitedPositionStatusEnum.Refused)
            {
                await _loggerService.Log(LogData.AsEvaluation($"Solicited test was refused", item));
            } 
            else if (item.SolicitedPositionStatus == SolicitedPositionStatusEnum.Approved)
            {
                await _loggerService.Log(LogData.AsEvaluation($"Solicited test was approved", item));
            }
        }
    }
}
