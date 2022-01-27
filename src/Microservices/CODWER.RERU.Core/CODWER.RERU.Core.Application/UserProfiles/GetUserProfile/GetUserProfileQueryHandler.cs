using System;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfiles.GetUserProfile 
{
    public class GetUserProfileQueryHandler : BaseHandler, IRequestHandler<GetUserProfileQuery, UserProfileDto> 
    {
        private readonly IDocumentStorageService _documentService;

        public GetUserProfileQueryHandler (ICommonServiceProvider commonServiceProvider, IDocumentStorageService documentService) : base (commonServiceProvider)
        {
            _documentService = documentService;
        }

        public async Task<UserProfileDto> Handle (GetUserProfileQuery request, CancellationToken cancellationToken) {
            var userProfile = await CoreDbContext.UserProfiles
                .Include(up => up.Avatar)
                .FirstOrDefaultAsync (u => u.Id == request.Id);

            var userProfDto = Mapper.Map<UserProfileDto>(userProfile);
            
            if (userProfile.Avatar == null) return userProfDto;
            
            var str = Convert.ToBase64String(await _documentService.GetDocument(userProfile.Avatar.DocumentStorageId));
            userProfDto.Avatar = str;
            
            return userProfDto;
        }
    }
}