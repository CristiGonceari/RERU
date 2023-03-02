using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation360.API.Config;
using CODWER.RERU.Evaluation360.Application.BLL.HomePage;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Evaluation360.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : BaseController
    {
        [HttpGet("evaluations-count")]
        public async Task<List<int>> GetNrEvaluations([FromQuery] GetNrEvaluationsQuery query)
        {
            return await Sender.Send(query);
        }
    }
}