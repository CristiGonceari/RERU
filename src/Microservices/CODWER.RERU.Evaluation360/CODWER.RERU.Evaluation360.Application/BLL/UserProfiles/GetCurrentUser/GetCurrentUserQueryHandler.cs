using AutoMapper;
using CODWER.RERU.Evaluation360.DataTransferObjects.UserProfile;
using CVU.ERP.ServiceProvider;
using MediatR;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation360.Application.BLL.UserProfiles.GetCurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserProfileDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly IMapper _mapper;

        public GetCurrentUserQueryHandler(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _currentUserProvider = currentUserProvider;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentUserProvider.Get();

            var userProfile = _appDbContext.UserProfiles.First(x => x.Id == int.Parse(currentUser.Id));

            return _mapper.Map<UserProfileDto>(userProfile);
        }
    }
}
