using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddDepartmentRoleRelation;
using CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddOrganizationalChartHead;
using CODWER.RERU.Personal.Application.DepartmentRoleRelations.GetDepartmentRoleRelations;
using CODWER.RERU.Personal.Application.DepartmentRoleRelations.GetOrganizationalChartContent;
using CODWER.RERU.Personal.Application.DepartmentRoleRelations.RemoveDepartmentRoleRelation;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentRoleRelationController : BaseController
    {
        [HttpGet]
        public async Task<DepartmentRoleRelationDto> GetDepartmentRoleRelations([FromQuery] GetDepartmentRoleRelationsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }


        [HttpPost]
        public async Task<int> CreateDepartmentRoleRelation([FromBody] AddDepartmentRoleRelationCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveDepartmentRoleRelation([FromRoute] int id)
        {
            var command = new RemoveDepartmentRoleRelationCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPost("head")]
        public async Task<int> AddOrganizationalChartHead([FromBody] AddOrganizationalChartHeadCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("organizational-chart-content/{id}")]
        public async Task<OrganizationalChartContentDto> GetDepartmentRoleRelations([FromRoute] int id)
        {
            var result = await Mediator.Send(
                new GetOrganizationalChartContentQuery
                {
                    OrganizationalChartId = id
                });

            return result;
        }
    }
}
