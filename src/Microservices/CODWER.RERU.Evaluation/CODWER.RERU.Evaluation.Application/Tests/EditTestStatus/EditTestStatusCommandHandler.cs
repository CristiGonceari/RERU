using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.EditTestStatus
{
    public class EditTestStatusCommandHandler : IRequestHandler<EditTestStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContex;
        private readonly IInternalNotificationService _internalNotificationService;

        public EditTestStatusCommandHandler(AppDbContext appDbContex, IInternalNotificationService internalNotificationService)
        {
            _internalNotificationService = internalNotificationService;
            _appDbContex = appDbContex;
        }

        public async Task<Unit> Handle(EditTestStatusCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContex.Tests
                .Include(x => x.TestTemplate)
                .FirstAsync(x => x.Id == request.TestId);

            test.TestStatus = request.Status;
            await _appDbContex.SaveChangesAsync();

            if (request.Status == TestStatusEnum.Verified)
            {
                await _internalNotificationService.AddNotification(test.UserProfileId, NotificationMessages.YourTestWasVerified);
            }

            return Unit.Value;
        }
    }
}
