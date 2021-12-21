using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.PrintTestReportList;
//using CODWER.RERU.Evaluation.Application.Services.GetPdfServices;
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
        //private readonly IGetTestPdf _testPdf;
        //private readonly IGetTestTemplatePdf _getTestTemplatePdf;
        //private readonly IGetQuestionUnitPdf _getQuestionPdf;
        private readonly IPdfService _pdfService;


        public TestPrintHtmlPage(
            //IGetTestPdf testPdf,
            //IGetTestTemplatePdf getTestTemplatePdf, 
            //IGetQuestionUnitPdf getQuestionPdf, 
            IPdfService pdfService)
        {
            //_testPdf = testPdf;
            //_getTestTemplatePdf = getTestTemplatePdf;
            //_getQuestionPdf = getQuestionPdf;
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

        [HttpGet("performing-test-pdf/{testId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetPerformingTestPdf([FromRoute] int testId)
        {
            var result = await _pdfService.PrintPerformingTestPdf(testId);
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
