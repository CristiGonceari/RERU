using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Modules.GetInternalModules;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.ServiceProvider.Models;
using MediatR;

namespace CODWER.RERU.Core.API.Controllers.Internal
{
    [Route("internal/api/[controller]")]
    [ApiController]
    public class ModuleController : BaseController
    {
        public ModuleController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<List<ModuleRolesDto>> GetApplicationModuleRoles()
        {
            return await Mediator.Send(new GetInternalModulesQuery());
        }
    }
}
