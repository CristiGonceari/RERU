using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {

        [HttpGet]
        public async Task<int> Test([FromRoute] int id)
        {
            return (int)await Mediator.Send(id);
        }
    }
}
