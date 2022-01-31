using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var userProfile = await _appDbContext.UserProfiles.FirstOrDefaultAsync(u => u.Id == request.Id);

            return _mapper.Map<UserProfileDto>(userProfile);
        }
    }
}