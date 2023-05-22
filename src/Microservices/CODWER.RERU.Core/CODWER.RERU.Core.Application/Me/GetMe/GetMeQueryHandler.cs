using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Me;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.ServiceProvider;
using MediatR;
using Microsoft.Extensions.Options;

namespace CODWER.RERU.Core.Application.Me.GetMe {
    public class GetMeQueryHandler : BaseHandler, IRequestHandler<GetMeQuery, MeDto> 
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly TenantDto _tenantDto;

        public GetMeQueryHandler (ICommonServiceProvider commonServiceProvider,
            ICurrentApplicationUserProvider currentUserProvider,
            IOptions<TenantDto> tenantOption) 
            : base (commonServiceProvider) {
            _currentUserProvider = currentUserProvider;
            _tenantDto = tenantOption.Value;

        }

        public async Task<MeDto> Handle (GetMeQuery request, CancellationToken cancellationToken) 
        {
            var me = new MeDto ();

            if (!_currentUserProvider.IsAuthenticated) return me;
            
            me.IsAuthenticated = true;
            var currentUser = await _currentUserProvider.Get ();
            me.User = Mapper.Map<ApplicationUserDto> (currentUser);
            me.Tenant = _tenantDto;

            var userProfile = AppDbContext.UserProfiles.FirstOrDefault(up => up.Id == int.Parse(currentUser.Id));

            userProfile.Token = request.Authorization;
            await AppDbContext.SaveChangesAsync();

            if (userProfile.DepartmentColaboratorId == null &&
                userProfile.RoleColaboratorId == null && 
                !string.IsNullOrEmpty(userProfile.Idnp)
                )
            {
                me.IsCandidateStatus = true;
            }
            else
            {
                me.IsCandidateStatus = false;
            }
           
            return me;
        }
    }
}