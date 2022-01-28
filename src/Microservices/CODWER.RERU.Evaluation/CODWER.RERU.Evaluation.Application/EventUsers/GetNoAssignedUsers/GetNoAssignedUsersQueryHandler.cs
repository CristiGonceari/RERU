using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities.StaticExtensions;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventUsers.GetNoAssignedUsers
{
    public class GetNoAssignedUsersQueryHandler : IRequestHandler<GetNoAssignedUsersQuery, List<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetNoAssignedUsersQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<UserProfileDto>> Handle(GetNoAssignedUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _appDbContext.EventUsers
                .Include(x => x.UserProfile)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.UserProfile.Id)
                .AsQueryable();

            var userProfiles = _appDbContext.UserProfiles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                userProfiles = userProfiles.FilterByNameAndIdnp(request.Keyword);
            }

            userProfiles = userProfiles.Where(x => !users.Any(s => s == x.Id));

            return _mapper.Map<List<UserProfileDto>>(userProfiles.ToList());
        }
    }
}
