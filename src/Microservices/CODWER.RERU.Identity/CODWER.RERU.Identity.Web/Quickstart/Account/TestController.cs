using CVU.ERP.Identity.Context;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
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
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly ISigningCredentialStore _signingCredentialStore;

        public TestController(IHttpContextAccessor httpContextAccessor, IdentityDbContext identityDbContext, IIdentityServerInteractionService interactionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityDbContext = identityDbContext;
            _interactionService = interactionService;
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

            var httpUser = HttpContext?.Session?.Keys?.ToList();

            string userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string userEmail = user?.FindFirst(ClaimTypes.Email)?.Value;
            string userName = user?.FindFirst(ClaimTypes.Name)?.Value;
            string Authentication = user?.FindFirst(ClaimTypes.Authentication)?.Value;
            string NameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string acces_token = user?.FindFirst("access_token")?.Value;
            string id_token = user?.FindFirst("id_token")?.Value;

            var identityUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            var identityContext = await _identityDbContext.UserTokens.FirstOrDefaultAsync(ut => ut.UserId == identityUser.Id);

            var identityTokens = _identityDbContext.UserTokens.ToList();

            var token = await HttpContext.GetTokenAsync("access_token");

            var signingCredentials = await _signingCredentialStore.GetSigningCredentialsAsync();

            var claimsList = new
            {
                user = user?.Claims,
                userEmail = userEmail,
                userName = userName,
                userId = userId,
                identityUser = identityUser,
                name = identityContext?.Name,
                value = identityContext?.Value,
                loginProvider = identityContext?.LoginProvider,
                token = token,
                identityTokens = identityTokens,
                signingCredentials = signingCredentials,
                httpUser = httpUser
            };

            return claimsList.ToString();
        }
    }
}
