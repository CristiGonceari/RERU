using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.DepartmentRoleContents.AddDepartmentRoleContent;
using CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentDashboard;
using CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentCalculated;
using CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentTemplate;
using CODWER.RERU.Personal.Application.DepartmentRoleContents.RemoveDepartmentRoleContent;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentRoleContentController : BaseController
    {
        [HttpGet("dashboard/{departmentId}")]
        public async Task<DepartmentDashboardDto> GetDepartmentRoleContentDashboard([FromRoute] int departmentId)
        {
            var query = new GetDepartmentDashboardQuery { DepartmentId = departmentId };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("template/{departmentId}")]
        public async Task<DepartmentRoleContentDto> GetDepartmentRoleContentTemplate([FromRoute] int departmentId)
        {
            var query = new GetDepartmentRoleContentTemplateQuery { DepartmentId = departmentId };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("calculated/{departmentId}")]
        public async Task<DepartmentRoleContentDto> GetDepartmentRoleContentCalculated([FromRoute] int departmentId)
        {
            var query = new GetDepartmentRoleContentCalculatedQuery { DepartmentId = departmentId };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateDepartmentRoleContentRelation([FromBody] AddEditDepartmentRoleContentDto dto)
        {
            var command = new AddDepartmentRoleContentCommand(dto);
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> CreateDepartment([FromRoute] int id)
        {
            var command = new RemoveDepartmentRoleContentCommand{Id = id};
            var result = await Mediator.Send(command);

            return result;
        }
    }
}
