using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Application.Common.Services;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CODWER.RERU.Core.DataTransferObjects.Profile;
using CVU.ERP.Module.Application.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfiles.GetMyUserProfileOverview
{
    public class GetMyUserProfileOverviewQueryHandler : BaseHandler, IRequestHandler<GetMyUserProfileOverviewQuery, UserProfileOverviewDto>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly IDocumentStorageService _documentService;

        public GetMyUserProfileOverviewQueryHandler(ICommonServiceProvider commonServicepProvider, ICurrentApplicationUserProvider currentApplicationUserProvider, IDocumentStorageService documentService) : base(commonServicepProvider)
        {
            _currentUserProvider = currentApplicationUserProvider;
            _documentService = documentService;
        }

        public async Task<UserProfileOverviewDto> Handle(GetMyUserProfileOverviewQuery request, CancellationToken cancellationToken)
        {
            var currentApplicationUser = await _currentUserProvider.Get();
            var userProfile = await CoreDbContext
                .UserProfiles.IncludeBasic()
                .FirstOrDefaultAsync(up => up.Id == Convert.ToInt32(currentApplicationUser.Id));

            var userProfDto = Mapper.Map<UserProfileOverviewDto>(userProfile);
            if (userProfile.Avatar != null)
            {
                var str = Convert.ToBase64String(await _documentService.GetDocument(userProfile.Avatar.DocumentStorageId));
                userProfDto.Avatar = str;
            }

            return userProfDto;
        }
    }
} 