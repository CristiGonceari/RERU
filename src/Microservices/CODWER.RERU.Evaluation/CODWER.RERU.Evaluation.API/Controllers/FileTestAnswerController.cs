using System;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.FileTestAnswers.AddFileTestAnswer;
using CODWER.RERU.Evaluation.Application.FileTestAnswers.GetFileTestAnswer;
using CODWER.RERU.Evaluation.Application.FileTestAnswers.GetQuestionFile;
using CODWER.RERU.Evaluation.DataTransferObjects.FileTestAnswers;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileTestAnswerController : BaseController
    {
        [HttpGet("{fileId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetQuestionFile([FromRoute] string fileId)
        {
            var query = new GetQuestionFileQuery { FileId = fileId };
            var result = await Mediator.Send(query);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, Uri.EscapeDataString(result.Name));
        }

        [HttpGet("file")]
        public async Task<GetTestFileDto> GetFileName([FromQuery] GetFileTestAnswerQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<string> AddFileAnswer([FromForm] AddFileTestAnswerCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
