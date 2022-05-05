using CODWER.RERU.Core.Application.RegistrationPageMessage.AddEditRegistrationPageMessage;
using CODWER.RERU.Core.Application.RegistrationPageMessage.GetRegistrationPageMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationPageController : BaseController
    {
        public RegistrationPageController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("message")]
        public Task<string> GetMessage()
        {
            return Mediator.Send(new GetRegistrationPageMessageQuery());
        }


        [HttpPatch]
        public Task<int> EditMessage([FromBody] AddEditRegistrationPageMessageCommand command)
        {
            return Mediator.Send(command);
        }
    }
}
