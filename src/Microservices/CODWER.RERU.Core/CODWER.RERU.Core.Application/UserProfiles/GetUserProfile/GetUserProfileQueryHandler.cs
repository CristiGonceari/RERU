using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfiles.GetUserProfile 
{
    public class GetUserProfileQueryHandler : BaseHandler, IRequestHandler<GetUserProfileQuery, UserProfileDto> 
    {

        public GetUserProfileQueryHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider){}

        public async Task<UserProfileDto> Handle (GetUserProfileQuery request, CancellationToken cancellationToken) 
        {
            var userProfile = await AppDbContext.UserProfiles
                .FirstOrDefaultAsync (u => u.Id == request.Id);

            var userProfDto = Mapper.Map<UserProfileDto>(userProfile);

            if (userProfile.MediaFileId == null) return userProfDto;
            
            return userProfDto;
        }
    }
}