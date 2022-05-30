using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Roles.AddRole;
using CODWER.RERU.Core.Application.Roles.BulkImportRoles;
using CODWER.RERU.Core.Application.Roles.GetRole;
using CODWER.RERU.Core.Application.Roles.GetRoles;
using CODWER.RERU.Core.Application.Roles.GetRolesValue;
using CODWER.RERU.Core.Application.Roles.PrintRoles;
using CODWER.RERU.Core.Application.Roles.RemoveRole;
using CODWER.RERU.Core.Application.Roles.UpdateRole;
using CODWER.RERU.Core.DataTransferObjects.Files;
using CODWER.RERU.Core.DataTransferObjects.Roles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : BaseController
    {
        public RoleController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        public async Task<RoleDto> GetRole([FromRoute] int id)
        {
            return await Mediator.Send(new GetRoleQuery { Id = id });
        }

        [HttpGet]
        public async Task<PaginatedModel<RoleDto>> GetRoles([FromQuery] GetRolesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddRole([FromBody] AddRoleCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpPatch]
        public async Task<int> EditRole([FromBody] UpdateRoleCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteRole([FromRoute] int id)
        {
            return await Mediator.Send(new RemoveRoleCommand { Id = id });
        }

        [HttpGet("select-values")]
        public async Task<List<SelectItem>> GetEvents()
        {
            var query = new GetRolesValueQuery();

            return await Mediator.Send(query);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintRolesPdf([FromBody] PrintRolesCommand command)
        {
            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("excel-import")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> ImportFromExcelFile([FromForm] BulkExcelImport dto)
        {
            var command = new BulkImportRolesCommand { Data = dto };

            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
