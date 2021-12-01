using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.PrintTestReportList;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestPrintHtmlPage : BaseController
    {
        [HttpGet("reports-list")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetDocuments([FromQuery] PrintTestReportListCommand query)
        {
            var result = await Mediator.Send(query);

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
