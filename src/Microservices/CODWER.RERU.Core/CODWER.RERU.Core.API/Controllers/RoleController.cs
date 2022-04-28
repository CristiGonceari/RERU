using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Roles.AddRole;
using CODWER.RERU.Core.Application.Roles.GetRole;
using CODWER.RERU.Core.Application.Roles.GetRoles;
using CODWER.RERU.Core.Application.Roles.PrintRoles;
using CODWER.RERU.Core.Application.Roles.RemoveRole;
using CODWER.RERU.Core.Application.Roles.UpdateRole;
using CODWER.RERU.Core.DataTransferObjects.Roles;
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
        public async Task<int> AddRole([FromBody] RoleDto request)
        {
            return await Mediator.Send(new AddRoleCommand { Data = request });
        }

        [HttpPatch]
        public async Task<int> EditRole([FromBody] RoleDto request)
        {
            return await Mediator.Send(new UpdateRoleCommand { Data = request });
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteRole([FromRoute] int id)
        {
            return await Mediator.Send(new RemoveRoleCommand { Id = id });
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintRolesPdf([FromBody] PrintRolesCommand command)
        {
            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
