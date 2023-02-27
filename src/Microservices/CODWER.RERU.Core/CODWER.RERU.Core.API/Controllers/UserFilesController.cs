using CODWER.RERU.Core.Application.UserFiles.AddUserFile;
using CODWER.RERU.Core.Application.UserFiles.GetUserFile;
using CODWER.RERU.Core.Application.UserFiles.GetUserFiles;
using CODWER.RERU.Core.Application.UserFiles.RemoveUserFiles;
using CODWER.RERU.Core.Application.UserFiles.PrintUserFiles;
using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserFilesController : BaseController 
    {
        public UserFilesController(IMediator mediator) : base(mediator)
        {

        }

        [HttpGet("{fileId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetUserFile([FromRoute] string fileId)
        {
            var query = new GetUserFileQuery {FileId = fileId};

            var result = await Mediator.Send(query);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpGet("files")]
        public async Task<PaginatedModel<GetFilesDto>> GetUserFiles([FromQuery] GetUserFilesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<string> AddUserFile([FromForm] AddUserFileCommand command)
        {  
            return await Mediator.Send(command);
        }

        [HttpDelete("{fileId}")]
        public async Task<Unit> DeleteFile([FromRoute] string fileId)
        {
            var command = new RemoveUserFileCommand { FileId = fileId };

            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintUserModules([FromBody] PrintUserFilesCommand command)
        {
            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
