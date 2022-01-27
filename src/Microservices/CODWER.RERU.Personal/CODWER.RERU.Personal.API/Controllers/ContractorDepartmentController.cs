using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.ContractorDepartments.AddContractorDepartment;
using CODWER.RERU.Personal.Application.ContractorDepartments.GetContractorDepartment;
using CODWER.RERU.Personal.Application.ContractorDepartments.GetContractorDepartments;
using CODWER.RERU.Personal.Application.ContractorDepartments.RemoveContractorDepartment;
using CODWER.RERU.Personal.Application.ContractorDepartments.UpdateContractorDepartment;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorDepartmentController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<ContractorDepartmentDto>> GetContractorDepartments([FromQuery] GetContractorDepartmentsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ContractorDepartmentDto> GetContractorDepartment([FromRoute] int id)
        {
            var query = new GetContractorDepartmentQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateContractorDepartment([FromBody] AddContractorDepartmentCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateContractorDepartment([FromBody] UpdateContractorDepartmentCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveContractorDepartment([FromRoute] int id)
        {
            var command = new RemoveContractorDepartmentCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}
