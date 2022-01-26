using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.DataTransferObjects.UserProfiles;
using MediatR;

namespace CODWER.RERU.Personal.Application.UserProfiles.GetOrCreateUserProfile
{
    public class GetOrCreateCurrentUserProfileCommandHandler : IRequestHandler<GetOrCreateUserProfileCommand, UserProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public GetOrCreateCurrentUserProfileCommandHandler(IUserProfileService userProfileService, IMapper mapper)
        {
            _userProfileService = userProfileService;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> Handle(GetOrCreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfile();

            return _mapper.Map<UserProfileDto>(currentUser);
        }
    }
}
