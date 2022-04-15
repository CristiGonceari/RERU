using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.InternalNotifications.MarkNotificationAsSeen
{
    public class MarkNotificationAsSeenCommandHandler : IRequestHandler<MarkNotificationAsSeenCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public MarkNotificationAsSeenCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(MarkNotificationAsSeenCommand request, CancellationToken cancellationToken)
        {
            var notification = await _appDbContext.Notifications
                .FirstOrDefaultAsync(x => x.Id == request.NotificationId);

            notification.Seen = true;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
