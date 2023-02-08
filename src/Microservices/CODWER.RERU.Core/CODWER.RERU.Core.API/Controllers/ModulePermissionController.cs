using CODWER.RERU.Core.Application.ModulePermissions.GetModulePermissions;
using CODWER.RERU.Core.Application.ModulePermissions.PrintModulePermissions;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
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

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintModulePermissions([FromBody] PrintModulePermissionsCommand command)
        {
            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}