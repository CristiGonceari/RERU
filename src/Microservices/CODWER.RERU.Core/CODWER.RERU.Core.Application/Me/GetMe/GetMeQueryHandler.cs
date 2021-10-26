using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Application.Common.Services;
using CODWER.RERU.Core.DataTransferObjects.Me;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Providers;
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

        public async Task<MeDto> Handle (GetMeQuery request, CancellationToken cancellationToken) {
            var me = new MeDto ();

            if (!_currentUserProvider.IsAuthenticated) return me;
            
            me.IsAuthenticated = true;
            var currentUser = await _currentUserProvider.Get ();
            me.User = Mapper.Map<ApplicationUserDto> (currentUser);
            me.Tenant = _tenantDto;

            return me;
        }
    }
}