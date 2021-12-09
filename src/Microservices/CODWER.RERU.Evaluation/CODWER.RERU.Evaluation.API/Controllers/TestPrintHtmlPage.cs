using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.PrintTestReportList;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services.GetPdfServices;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestPrintHtmlPage : BaseController
    {
        private readonly IGetTestPdf _testPdf;
        public readonly IGetTestTemplatePdf _getTestTemplatePdf;

        public TestPrintHtmlPage(IGetTestPdf testPdf, IGetTestTemplatePdf getTestTemplatePdf)
        {
            _testPdf = testPdf;
            _getTestTemplatePdf = getTestTemplatePdf;
        }


        [HttpGet("reports-list")]
        [IgnoreResponseWrap]

        public async Task<IActionResult> GetDocuments([FromQuery] PrintTestReportListCommand query)
        {
            var result = await Mediator.Send(query);

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpGet("test-pdf/{testId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetTestPdf([FromRoute] int testId)
        {
            var result = await _testPdf.PrintTestPdf(testId);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            return File(result.Content, result.ContentType, result.Name);

        }

        [HttpGet("print-test-type-{id}")]
        [IgnoreResponseWrap]

        public async Task<IActionResult> PrintTestTypePdf([FromRoute] int id)
        {
            var result = await _getTestTemplatePdf.PrintTestTemplatePdf(id);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
