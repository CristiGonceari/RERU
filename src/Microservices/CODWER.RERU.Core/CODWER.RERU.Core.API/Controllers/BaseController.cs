using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CODWER.RERU.Core.API.Controllers {
    [ApiController]
    public abstract class BaseController : Controller {
        public BaseController (IMediator mediator) {
            _mediator = mediator;
        }
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator> ());
    }
}