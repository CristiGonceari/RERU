using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.RequiredDocuments.AddEditRequiredDocument;
using CODWER.RERU.Evaluation.Application.RequiredDocuments.DeleteRequiredDocument;
using CODWER.RERU.Evaluation.Application.RequiredDocuments.GetRequiredDocument;
using CODWER.RERU.Evaluation.Application.RequiredDocuments.GetRequiredDocuments;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.RequiredDocuments.PrintRequiredDocuments;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequiredDocumentController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<RequiredDocumentDto> GetRequiredDocument([FromRoute] int id)
        {
            var query = new GetRequiredDocumentQuery { Id = id };
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<PaginatedModel<RequiredDocumentDto>> GetRequiredDocuments([FromQuery] GetRequiredDocumentsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddEditRequiredDocument([FromForm] AddEditRequiredDocumentsCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteRequiredDocument([FromRoute] int id)
        {
            var command = new DeleteRequiredDocumentsCommand { Id = id };
            return await Mediator.Send(command);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintRequiredDocuments([FromBody] PrintRequiredDocumentsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
