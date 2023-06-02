using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.HomePage;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : BaseController
    {
        [HttpGet("tests-count")]
        public async Task<List<int>> GetNrTests([FromQuery] GetNrTestsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}