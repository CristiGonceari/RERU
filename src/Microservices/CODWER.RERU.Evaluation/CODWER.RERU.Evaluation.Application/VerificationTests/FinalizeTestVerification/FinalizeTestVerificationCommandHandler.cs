using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.VerificationTests.AutoCheckTestScore;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.FinalizeTestVerification
{
    public class FinalizeTestVerificationCommandHandler : IRequestHandler<FinalizeTestVerificationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;

        public FinalizeTestVerificationCommandHandler(AppDbContext appDbContext, IMediator mediator, INotificationService notificationService, IInternalNotificationService internalMotificationService)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
            _notificationService = notificationService;
            _internalNotificationService = internalMotificationService;
        }

        public async Task<Unit> Handle(FinalizeTestVerificationCommand request, CancellationToken cancellationToken)
        {
            var testToFinalize = await _appDbContext.Tests.FirstAsync(x => x.Id == request.TestId);

            testToFinalize.TestStatus = TestStatusEnum.Verified;

            await _mediator.Send(new AutoCheckTestScoreCommand { TestId = request.TestId });

            await _appDbContext.SaveChangesAsync();

            await _internalNotificationService.AddNotification(testToFinalize.UserProfileId, NotificationMessages.YourTestWasVerified);

            await SendEmailNotification(testToFinalize);

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(Test testToFinalize)
        {
            var user = await _appDbContext.UserProfiles
                .FirstOrDefaultAsync(x => x.Id == testToFinalize.UserProfileId);

            var test = await _appDbContext.Tests
                .Include(x => x.TestType)
                .FirstOrDefaultAsync(x => x.Id == testToFinalize.Id);

            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            var template = await File.ReadAllTextAsync(path);

            template = template
                .Replace("{user_name}", user.FirstName + " " + user.LastName)
                .Replace("{email_message}", await GetTableContent(test));

            var emailData = new EmailData()
            {
                subject = "Rezultatul testului",
                body = template,
                from = "Do Not Reply",
                to = user.Email
            };

            await _notificationService.Notify(emailData, NotificationType.LocalNotification);

            return Unit.Value;
        }

        private async Task<string> GetTableContent(Test test)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Testul ""{test.TestType.Name}"" a fost verificat.</p>
                <p style=""font-size: 22px;font-weight: 300;"">Ați acumulat {test.AccumulatedPercentage}% din 100 %.</p>
                <p style=""font-size: 22px;font-weight: 300;"">Testul a fost trecut {EnumMessages.EnumMessages.GetTestResultStatus(test.ResultStatus)}.</p> ";

            return content;
        }
    }
}
