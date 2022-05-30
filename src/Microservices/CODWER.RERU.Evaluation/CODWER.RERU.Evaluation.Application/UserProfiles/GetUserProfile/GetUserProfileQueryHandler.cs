using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.GetUserProfile 
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetUserProfileQueryHandler (IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<UserProfileDto> Handle (GetUserProfileQuery request, CancellationToken cancellationToken) 
        {
            var userProfile = await _appDbContext.UserProfiles
                .Include(x => x.Department)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(u => u.Id == request.Id);

            var userProfDto = _mapper.Map<UserProfileDto>(userProfile);

            return userProfDto;
        }
    }
}