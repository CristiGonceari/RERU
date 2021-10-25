using System.Threading.Tasks;
using CVU.ERP.Module.Application.Models;
using Microsoft.AspNetCore.Http;

namespace CVU.ERP.Module.Application.Providers
{
    ///<summary>
    /// Default implementation of the ICurrentApplicationUserProvider
    ///</summary>
    public class CurrentApplicationUserProvider : ICurrentApplicationUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationUserProvider _applicationUserProvider;
        public CurrentApplicationUserProvider(IHttpContextAccessor httpContextAccessor, IApplicationUserProvider applicationUserProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationUserProvider = applicationUserProvider;
        }

        ///<summary>
        /// Returns current application user.
        /// If the user is not authenticated, it will return a default application user with IsAnonymous = true
        ///</summary>
        public async Task<ApplicationUser> Get()
        {
            if (IsAuthenticated)
            {
                return await _applicationUserProvider.Get(IdentityId, IdentityProvider);
            }
            return new ApplicationUser();
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string IdentityId
        {
            get
            {
                var identityUser = _httpContextAccessor.HttpContext.User;
                return identityUser.FindFirst("sub")?.Value;
            }
        }

        public string IdentityProvider
        {
            get
            {
                var identityUser = _httpContextAccessor.HttpContext.User;
                return identityUser.FindFirst("idp")?.Value;
            }
        }
    }
}