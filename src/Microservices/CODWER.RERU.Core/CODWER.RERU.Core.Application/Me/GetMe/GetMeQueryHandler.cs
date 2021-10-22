using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Application.Common.Services;
using CODWER.RERU.Core.DataTransferObjects.Me;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CODWER.RERU.Core.Application.Me.GetMe {
    public class GetMeQueryHandler : BaseHandler, IRequestHandler<GetMeQuery, MeDto> {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly TenantDto _tenantDto;
        private readonly IDocumentStorageService _documentService;

        public GetMeQueryHandler (ICommonServiceProvider commonServicepProvider, ICurrentApplicationUserProvider currentUserProvider,
            IDocumentStorageService documentService,
            IOptions<TenantDto> tenantOption) : base (commonServicepProvider) {
            _currentUserProvider = currentUserProvider;
            _tenantDto = tenantOption.Value;
            _documentService = documentService;

        }

        public async Task<MeDto> Handle (GetMeQuery request, CancellationToken cancellationToken) {
            var me = new MeDto ();

            if (_currentUserProvider.IsAuthenticated) {
                me.IsAuthenticated = true;

                var currentUser = await _currentUserProvider.Get ();



                // var userProfile = await CoreDbContext
                //     .UserProfiles
                //     .Include (up => up.Avatar)
                //     .FirstOrDefaultAsync (up => up.Identities.Any (upi => upi.Identificator == currentUser.Id));

                me.User = Mapper.Map<ApplicationUserDto> (currentUser);

                // if (currentUser.Id != "test-id-1") {
                // if (userProfile.Avatar != null) {
                //     var str = Convert.ToBase64String (await _documentService.GetDocument (userProfile.Avatar.DocumentStorageId));
                //     me.User.Avatar = str;
                // }
                // }
                me.Tenant = _tenantDto;
            }
            return me;
        }
    }
}