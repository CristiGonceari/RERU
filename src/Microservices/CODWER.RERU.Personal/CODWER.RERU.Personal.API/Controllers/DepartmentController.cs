using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Departments.BulkImportDepartments;
using CODWER.RERU.Personal.Application.Departments.GetDepartment;
using CODWER.RERU.Personal.Application.Departments.GetDepartments;
using CODWER.RERU.Personal.Application.Departments.PrintDepartments;
using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        //[HttpPost]
        //public async Task<int> CreateDepartment([FromBody] AddDepartmentCommand command)
        //{
        //    var result = await Mediator.Send(command);

        //    return result;
        //}

        //[HttpPatch]
        //public async Task<Unit> UpdateDepartment([FromBody] UpdateDepartmentCommand command)
        //{
        //    var result = await Mediator.Send(command);

        //    return result;
        //}

        //[HttpDelete("{id}")]
        //public async Task<Unit> RemoveDepartment([FromRoute] int id)
        //{
        //    var command = new RemoveDepartmentCommand { Id = id };
        //    var result = await Mediator.Send(command);

        //    return result;
        //}

        [HttpPut("excel-import")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> ImportFromExcelFile([FromForm] ExcelDataDto dto)
        {
            var command = new BulkImportDepartmentsCommand { Data = dto };

            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintQuestionUnits([FromBody] PrintDepartmentsCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
