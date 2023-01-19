using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.EmployeeFunctions.GetEmployeeFunction;
using CODWER.RERU.Personal.Application.EmployeeFunctions.GetEmployeeFunctions;
using CODWER.RERU.Personal.Application.EmployeeFunctions.ImportEmployeeFunctions;
using CODWER.RERU.Personal.Application.EmployeeFunctions.PrintEmployeeFunctions;
using CODWER.RERU.Personal.DataTransferObjects.EmployeeFunctions;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeFunctionController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<EmployeeFunctionDto>> GetEmployeeFunctions([FromQuery] GetEmployeeFunctionsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<EmployeeFunctionDto> GetEmployeeFunction([FromRoute] int id)
        {
            var query = new GetEmployeeFunctionQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPut("excel-import")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> ImportFromExcelFile([FromForm] ExcelDataDto dto)
        {
            var command = new ImportEmployeeFunctionsCommand { Data = dto };

            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintEmployeeFunctions([FromBody] PrintEmployeeFunctionsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
