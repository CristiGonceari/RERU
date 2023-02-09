using CODWER.RERU.Evaluation360.API.Config;
using CODWER.RERU.Evaluation360.Application.BLL.UserGuide.GetUserGuide;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation360.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGuideController : BaseController
    {
        [HttpGet("ghid")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetFile()
        {
            var command = new GetUserGuidePdfQuery { };

            var result = await Sender.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}