using System.Collections.Generic;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.PrintTestReportList;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestPrintHtmlPage : BaseController
    {
        private readonly IPdfService _pdfService;

        public TestPrintHtmlPage(IPdfService pdfService)
        {
            _pdfService = pdfService;
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
            var result = await _pdfService.PrintTestPdf(testId);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpGet("performing-test-pdf")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetPerformingTestPdf([FromQuery] List<int> testsIds)
        {
            var result = await _pdfService.PrintPerformingTestPdf(testsIds);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpGet("print-test-type-{id}")]
        [IgnoreResponseWrap]

        public async Task<IActionResult> PrintTestTypePdf([FromRoute] int id)
        {
            var result = await _pdfService.PrintTestTemplatePdf(id);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpGet("question-pdf/{questionId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetQuestionPdf([FromRoute] int questionId)
        {
            var result = await _pdfService.PrintQuestionUnitPdf(questionId);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
