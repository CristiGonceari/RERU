using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.ModuleRoles.AddModuleRole;
using CODWER.RERU.Core.Application.ModuleRoles.DeleteModuleRole;
using CODWER.RERU.Core.Application.ModuleRoles.EditModuleRole;
using CODWER.RERU.Core.Application.ModuleRoles.GetAllModuleRoles;
using CODWER.RERU.Core.Application.ModuleRoles.GetModuleRoleDetails;
using CODWER.RERU.Core.Application.ModuleRoles.GetModuleRoleForEdit;
using CODWER.RERU.Core.Application.ModuleRoles.GetModuleRolesForSelect;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Core.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ModuleRoleController : BaseController {

        public ModuleRoleController (IMediator mediator) : base (mediator) { }

        [HttpGet]
        public async Task<PaginatedModel<ModuleRoleRowDto>> GetRoles ([FromQuery] GetModuleRolesQuery query) {
            var mediatorResponse = await Mediator.Send (query);
            return mediatorResponse;
        }

        [HttpPost]
        public Task<Unit> CreateModuleRole ([FromBody] AddEditModuleRoleDto moduleRole) {
            return Mediator.Send (new CreateModuleRoleCommand (moduleRole));
        }

        [HttpPut]
        public Task EditModuleRole ([FromBody] AddEditModuleRoleDto moduleRole) {
            return Mediator.Send (new EditModuleRoleCommand (moduleRole));
        }

        [HttpGet ("{id:int}")]
        public Task<ModuleRoleDto> GetModuleRoleDetails ([FromRoute] int id) {
            var getRoleModuleDetails = new GetModuleRoleDetailsQuery (id);
            return Mediator.Send (getRoleModuleDetails);
        }

        [HttpGet ("{id:int}/select-items")]
        public Task<IEnumerable<SelectItem>> GetModuleRoleForSelect ([FromRoute] int id) {
            return Mediator.Send (new GetModuleRoleSelectItemsQuery (id));
        }

        [HttpGet ("{id:int}/edit")]
        public Task<AddEditModuleRoleDto> GetModuleRoleForEdit ([FromRoute] int id) {
            var getRoleModuleDetails = new GetModuleRoleForEditQuery (id);
            return Mediator.Send (getRoleModuleDetails);
        }

        [HttpDelete ("{id}")]
        public Task DeleteModuleRole ([FromRoute] int id) {
            var deleteModuleRole = new DeleteModuleRoleCommand (id);
            return Mediator.Send (deleteModuleRole);
        }
    }
}