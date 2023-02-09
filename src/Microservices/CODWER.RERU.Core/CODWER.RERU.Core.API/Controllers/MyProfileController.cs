using CODWER.RERU.Core.Application.MyProfile.Files.AddFiles;
using CODWER.RERU.Core.Application.MyProfile.Files.GetFiles;
using CODWER.RERU.Core.Application.MyProfile.Files.PrintFiles;
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
    public class MyProfileController : BaseController
    {
        public MyProfileController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("files")]
        public async Task<PaginatedModel<GetFilesDto>> GetUserFiles([FromQuery] GetFilesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost("file")]
        public async Task<string> AddUserFile([FromForm] AddFilesCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintCurrentUserModules([FromBody] PrintFilesCommand command)
        {
            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
