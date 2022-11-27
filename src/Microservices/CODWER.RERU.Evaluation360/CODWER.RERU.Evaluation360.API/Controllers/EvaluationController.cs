using System.Threading.Tasks;
using CODWER.RERU.Evaluation360.API.Config;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetMyEvaluations;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Evaluation360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvalutaionController : BaseController
    {
        [HttpGet("mine")]
        public async Task<PaginatedModel<EvaluationRowDto>> GetMyEvaliations([FromQuery] GetMyEvaluationsQuery query)
        {
            return await Sender.Send(query);
        }
    }
}