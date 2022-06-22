using CODWER.RERU.Core.Application.Users.GetPersonalFile;
using CODWER.RERU.Core.Application.Users.InregistrateUser;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Users.SendEmailVerificationCode;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InregistrateUserController : BaseController
    {
        public InregistrateUserController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public Task<int> InregistrateUser([FromBody] InregistrateUserCommand command)
        {
            return Mediator.Send(command);
        }

        [HttpPost("verify-code")]
        public Task<int> VerifyEmail([FromBody] SendEmailVerificationCodeCommand command)
        {
            return Mediator.Send(command);
        }


        [HttpGet("personal-file")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetFile()
        {
            var command = new GetPersonalFileQuery { };

            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
