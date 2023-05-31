using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CODWER.RERU.Identity.Web.Quickstart.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class MPassLogoutController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public IConfiguration Configuration { get; }


        public MPassLogoutController( AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            Configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            ClaimsPrincipal user = User;

            string userEmail = user?.FindFirst(ClaimTypes.Email)?.Value;

            var userProfile = await _appDbContext.UserProfiles.FirstOrDefaultAsync(up => up.Email == userEmail);

            var redirectUrl = Configuration.GetValue<string>("AllowedCorsOrigins") + "/ms/reru-identity-new/connect/endsession?id_token_hint=" + userProfile.Token;

            return Redirect(redirectUrl);
        }

        [HttpGet("info")]
        public async Task<string> GetInfoAsync()
        {
            ClaimsPrincipal user = User;

            string userEmail = user?.FindFirst(ClaimTypes.Email)?.Value;

            var userProfileToken = "";

            if (!string.IsNullOrEmpty(userEmail))
            {
                userProfileToken = _appDbContext.UserProfiles.FirstOrDefault(up => up.Email == userEmail).Token;   
            }

            var redirectUrl = Configuration.GetValue<string>("AllowedCorsOrigins") + "/ms/reru-identity-new/connect/endsession?id_token_hint=" + userProfile.Token;

            return new { userEmail = userEmail, userProfileToken = userProfileToken, redirectUrl = redirectUrl }.ToString();
        }
    }
}
