using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.ModulePermissions.GetAllModulePermissions;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModulePermissionController : BaseController
    {

        public ModulePermissionController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<PaginatedModel<ModulePermissionRowDto>> GetPermissions([FromQuery] GetModulePermissionsQuery query)
        {
            var mediatorResponse = await Mediator.Send(query);
            return mediatorResponse;
        }
    }
}