using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Modules.GetAvailableModules;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Core.API.Controllers 
{
    [ApiController]
    [Route ("api/[controller]")]
    public class ModuleController : BaseController 
    {

        public ModuleController (IMediator mediator) : base (mediator) { }

        [HttpGet]
        public Task<IEnumerable<ModuleDto>> GetModules () {
            return Mediator.Send (new GetAvailableModulesQuery ());
        }
    }
}