using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Reports.GetReports;
using CODWER.RERU.Personal.Application.Reports.PrintReports;
using CODWER.RERU.Personal.DataTransferObjects.Reports;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<ReportItemDto>> GetDocuments([FromQuery] GetReportsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("reports-list")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetDocuments([FromQuery] PrintReportsCommand query)
        {
            var result = await Mediator.Send(query);

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
