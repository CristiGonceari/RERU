using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Identity.Web.Quickstart.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TestController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<string> GetData()
        {
            var isAuth = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            var identityUser = _httpContextAccessor.HttpContext.User;

            return "lol0"+isAuth+"\nlol1   "+identityUser.FindFirst("sub")?.Value +"\nlol2 "+ identityUser.FindFirst("idp")?.Value;
        }
    }
}
