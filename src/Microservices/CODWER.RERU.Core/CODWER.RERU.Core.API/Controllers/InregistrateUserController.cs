using CODWER.RERU.Core.Application.Users.InregistrateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    }
}
