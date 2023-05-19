using CVU.ERP.Identity.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CODWER.RERU.Identity.Web.Quickstart.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IdentityDbContext _identityDbContext;

        public TestController(IHttpContextAccessor httpContextAccessor, IdentityDbContext identityDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityDbContext = identityDbContext;
        }

        [HttpGet]
        public async Task<string> GetData()
        {
            var isAuth = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            var identityUser = _httpContextAccessor.HttpContext.User;

            return "lol0"+isAuth+"\nlol1   "+identityUser.FindFirst("sub")?.Value +"\nlol2 "+ identityUser.FindFirst("idp")?.Value;
        }

        [HttpGet("user")]
        public async Task<string> GetUserInfo()
        {
            ClaimsPrincipal user = User;

            string userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string userEmail = user?.FindFirst(ClaimTypes.Email)?.Value;
            string userName = user?.FindFirst(ClaimTypes.Name)?.Value;

            var identityContext = await _identityDbContext.UserTokens.FirstOrDefaultAsync(ut => ut.UserId == userId);

            var claimsList = new
            {
                user = user,
                userEmail = userEmail,
                userName = userName,
                userId = userId,
                name = identityContext?.Name,
                value = identityContext?.Value,
                loginProvider = identityContext?.LoginProvider
            };

            return claimsList.ToString();
        }
    }
}
