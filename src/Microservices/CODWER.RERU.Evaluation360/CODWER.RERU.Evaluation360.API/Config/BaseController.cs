using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CODWER.RERU.Evaluation360.API.Config
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : Controller
    {
        private ISender _sender;
        protected ISender Sender => _sender ?? (_sender = HttpContext.RequestServices.GetService<ISender>());
    }
}
