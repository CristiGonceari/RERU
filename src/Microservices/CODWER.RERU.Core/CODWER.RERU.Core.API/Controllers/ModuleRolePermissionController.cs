using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.ModuleRolePermissions.GetModuleRolePermissions;
using CODWER.RERU.Core.Application.ModuleRolePermissions.GetModuleRolePermissionsForUpdate;
using CODWER.RERU.Core.Application.ModuleRolePermissions.UpdateModuleRolePermissions;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using CODWER.RERU.Core.DataTransferObjects.ModuleRolePermissions;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleRolePermissionController : BaseController
    {

        public ModuleRolePermissionController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<PaginatedModel<ModulePermissionRowDto>> GetModuleRolePermissions([FromQuery] GetModuleRolePermissionsQuery query)
        {
            var mediatorResponse = await Mediator.Send(query);
            return mediatorResponse;
        }

        [HttpGet("{id}/edit")]
        public async Task<List<ModuleRolePermissionGrantedRowDto>> GetModuleRolePermissionsForUpdate([FromRoute] int id)
        {
            return await Mediator.Send(new GetModuleRolePermissionsForUpdateQuery(id));
        }

        [HttpPut]
        public Task UpdateModuleRolePermissions([FromBody] ModuleRolePermissionsDto moduleRolePermissions)
        {
            return Mediator.Send(new UpdateModuleRolePermissionsCommand(moduleRolePermissions));
        }
    }
}