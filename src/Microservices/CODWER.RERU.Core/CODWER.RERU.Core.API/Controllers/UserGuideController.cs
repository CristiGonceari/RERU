using CODWER.RERU.Core.Application.UserGuide.GetUserGuide;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGuideController : BaseController
    {
        public UserGuideController(IMediator mediator) : base(mediator) { }

        [HttpGet("ghid")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetFile()
        {
            var command = new GetUserGuidePdfQuery { };

            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
