using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Departments.AddDepartment;
using CODWER.RERU.Personal.Application.Departments.GetDepartment;
using CODWER.RERU.Personal.Application.Departments.GetDepartments;
using CODWER.RERU.Personal.Application.Departments.RemoveDepartment;
using CODWER.RERU.Personal.Application.Departments.UpdateDepartment;
using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<DepartmentDto>> GetDepartments([FromQuery] GetDepartmentsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<DepartmentDto> GetDepartment([FromRoute] int id)
        {
            var query = new GetDepartmentQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateDepartment([FromBody] AddDepartmentCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateDepartment([FromBody] UpdateDepartmentCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveDepartment([FromRoute] int id)
        {
            var command = new RemoveDepartmentCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}
