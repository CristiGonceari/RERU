using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Contractors.ContractorFile.AddContractorFile;
using CODWER.RERU.Personal.Application.Contractors.ContractorFile.DeleteContractorFile;
using CODWER.RERU.Personal.Application.Contractors.ContractorFile.GetContractorFile;
using CODWER.RERU.Personal.Application.TemplateParsers;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseController
    {
        private readonly ITemplateConvertor _templateConvertor;

        public FileController(ITemplateConvertor templateConvertor)
        {
            _templateConvertor = templateConvertor;
        }

        [HttpPost]
        public async Task<int> CreateFile([FromForm] AddFileDto dto)
        {
            var command = new AddContractorFileCommand { Data = dto };

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{fileId}")]
        public async Task<Unit> DeleteFile([FromRoute] int fileId)
        {
            var command = new DeleteContractorFileCommand {FileId = fileId};
          
            var result =  await Mediator.Send(command);

            return result;
        }

        [HttpGet("{fileId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetFile([FromRoute] int fileId)
        {
            var command = new GetContractorFileQuery {FileId = fileId};

            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("html-to-pdf")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetPdf([FromForm] IFormFile file)
        {
            var result = await _templateConvertor.GetPdfFromFile(file);

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("html-string-to-pdf")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetPdfFromString([FromBody] HtmlContentDto content)
        {
            var result = await _templateConvertor.GetPdfFromHtmlString(content);
            
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
