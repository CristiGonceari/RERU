using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Users.GetUserDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TryLongRequestController : BaseController
    {
        public TryLongRequestController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public string GetUserDetails()
        {
            Thread.Sleep(60000);
            return "Job Done";
        }
    }
}
