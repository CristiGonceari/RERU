using MediatR;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventUsers.GetListOfEventUsers
{
    public class GetListOfEventUsersQueryHandler : IRequestHandler<GetListOfEventUsersQuery, List<int>>
    {
        private readonly AppDbContext _appDbContext;

        public GetListOfEventUsersQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<int>> Handle(GetListOfEventUsersQuery request, CancellationToken cancellationToken)
        {
            var eventUserIds =  _appDbContext.EventUsers.Where(eu => eu.EventId == request.EventId).Select(eu => eu.UserProfileId).ToList();

            return eventUserIds;
        }
    }
}
