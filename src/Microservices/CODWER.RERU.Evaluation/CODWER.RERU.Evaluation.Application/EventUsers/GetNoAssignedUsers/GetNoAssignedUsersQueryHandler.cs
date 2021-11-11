using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;

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
            var answer = new List<UserProfile>();

            var users = _appDbContext.EventUsers
                .Include(x => x.UserProfile)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.UserProfile.Id)
                .AsQueryable();

            var userProfiles = _appDbContext.UserProfiles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                userProfiles = userProfiles.Where(x => x.FirstName.Contains(request.Keyword) || x.LastName.Contains(request.Keyword) || x.Patronymic.Contains(request.Keyword));
            }

            userProfiles = userProfiles.Where(x => !users.Any(s => s == x.Id));

            answer = await userProfiles.ToListAsync();

            return _mapper.Map<List<UserProfileDto>>(answer);
        }
    }
}
