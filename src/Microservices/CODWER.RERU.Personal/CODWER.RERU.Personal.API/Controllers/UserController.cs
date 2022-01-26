using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DefaultController : BaseController
    {
        [HttpGet]
        public Task<string> GetStatus()
        {
            return Task.Run(() => "Management Personnel Microservice Running");
        }
    }
}
