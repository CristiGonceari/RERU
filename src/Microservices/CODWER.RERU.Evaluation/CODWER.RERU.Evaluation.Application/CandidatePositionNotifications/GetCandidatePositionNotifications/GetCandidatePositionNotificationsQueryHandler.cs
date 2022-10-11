using MediatR;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.GetCandidatePositionNotifications
{
    public class GetCandidatePositionNotificationsQueryHandler : IRequestHandler<GetCandidatePositionNotificationsQuery, List<int>>
    {
        private readonly AppDbContext _appDbContext;

        public GetCandidatePositionNotificationsQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<int>> Handle(GetCandidatePositionNotificationsQuery request, CancellationToken cancellationToken)
        {
            return _appDbContext.CandidatePositionNotifications
                .Where(x => x.CandidatePositionId == request.CandidatePositionId)
                .Select(x => x.UserProfileId)
                .ToList();
        }
    }
}
