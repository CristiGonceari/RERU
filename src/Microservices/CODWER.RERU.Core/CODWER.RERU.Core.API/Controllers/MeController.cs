using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Me.GetMe;
using CODWER.RERU.Core.DataTransferObjects.Me;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Core.API.Controllers 
{
    [ApiController]
    [Route ("api/[controller]")]
    public class MeController : BaseController 
    {
        public MeController (IMediator mediator) : base (mediator) { }

        [HttpGet]
        public async Task<MeDto> GetMe () {
            return await Mediator.Send (new GetMeQuery ());
        }
    }
}