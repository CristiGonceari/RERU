using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using MediatR;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.GetNotificatedUsers
{
    public class GetNotificatedUsersQueryHandler : IRequestHandler<GetNotificatedUsersQuery, List<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetNotificatedUsersQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<UserProfileDto>> Handle(GetNotificatedUsersQuery request, CancellationToken cancellationToken)
        {
            return _appDbContext.CandidatePositionNotifications
                .Where(x => x.CandidatePositionId == request.CandidatePositionId)
                .Select(x => x.UserProfile)
                .Select(x => _mapper.Map<UserProfileDto>(x))
                .ToList();
        }
    }
}
