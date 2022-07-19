﻿using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.OrganizationRoles.AddOrganizationRole;
using CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRole;
using CODWER.RERU.Personal.Application.OrganizationRoles.GetOrganizationRoles;
using CODWER.RERU.Personal.Application.OrganizationRoles.ImportOrganizationRoles;
using CODWER.RERU.Personal.Application.OrganizationRoles.RemoveOrganizationRole;
using CODWER.RERU.Personal.Application.OrganizationRoles.UpdateOrganizationRole;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationRoleController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<RoleDto>> GetOrganizationRoles([FromQuery] GetRolesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<RoleDto> GetOrganizationRole([FromRoute] int id)
        {
            var query = new GetRoleQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateOrganizationRole([FromBody] AddRoleCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateOrganizationRole([FromBody] UpdateRoleCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveOrganizationRole([FromRoute] int id)
        {
            var command = new RemoveRoleCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPut("excel-import")]
        public async Task ImportFromExcelFile([FromForm] ExcelDataDto dto)
        {
            var command = new ImportRolesCommand { Data = dto };

            await Mediator.Send(command);
        }
    }
}
