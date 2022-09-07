using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.VerificationTests.AutoCheckTestScore;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.FinalizeTestVerification
{
    public class FinalizeTestVerificationCommandHandler : IRequestHandler<FinalizeTestVerificationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;

        public FinalizeTestVerificationCommandHandler(AppDbContext appDbContext, IMediator mediator, INotificationService notificationService, IInternalNotificationService internalNotificationService)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
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
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == testToFinalize.Id);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Rezultatul testului",
                To = user.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.FullName },
                    { "{email_message}", await GetTableContent(test) }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent(Test test)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">testul ""{test.TestTemplate.Name}"" a fost verificat.</p>
                <p style=""font-size: 22px;font-weight: 300;"">Ați acumulat {test.AccumulatedPercentage}% din 100 %.</p>";

            return content;
        }
    }
}
