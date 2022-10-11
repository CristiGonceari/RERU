using CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.AddCandidatePositionNotification;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositionNotifications;
using MediatR;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class CandidatePositionNotificationService : ICandidatePositionNotificationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;
        private readonly List<int> _addedUsersIds = new();

        public CandidatePositionNotificationService(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        public async Task AddCandidatePositionNotification(List<int> userProfileIds, int candidatePositionId)
        {
            foreach (var userId in userProfileIds)
            {
                var exist = ExistNotificationUser(userId, candidatePositionId);

                if (!exist)
                {
                    var command = new AddCandidatePositionNotificationCommand
                    {
                        Data = new CandidatePositionNotificationDto
                        {
                            UserProfileId = userId,
                            CandidatePositionId = candidatePositionId
                        }
                    };

                    var result = await _mediator.Send(command);

                    _addedUsersIds.Add(result);
                }
                else
                {
                    _addedUsersIds.Add(userId);
                }
            }

            await UnassignUsersFromCandidateNotification(candidatePositionId);
        }

        private bool ExistNotificationUser(int userId, int candidatePositionId) =>
            _appDbContext.CandidatePositionNotifications.Any(x =>
                x.CandidatePositionId == candidatePositionId && x.UserProfileId == userId);

        private async Task UnassignUsersFromCandidateNotification(int candidatePositionId)
        {
            var itemsToDelete = _appDbContext.CandidatePositionNotifications
                .Where(eu => _addedUsersIds.All(p2 => p2 != eu.UserProfileId) && eu.CandidatePositionId == candidatePositionId);

            if (itemsToDelete.Any())
            {
                _appDbContext.CandidatePositionNotifications.RemoveRange(itemsToDelete);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
