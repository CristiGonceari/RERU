using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Departments.AddDepartment;
using CODWER.RERU.Core.Application.Departments.BulkImportDepartments;
using CODWER.RERU.Core.Application.Departments.GetDepartment;
using CODWER.RERU.Core.Application.Departments.GetDepartments;
using CODWER.RERU.Core.Application.Departments.GetDepartmentsValue;
using CODWER.RERU.Core.Application.Departments.PrintDepartment;
using CODWER.RERU.Core.Application.Departments.RemoveDepartment;
using CODWER.RERU.Core.Application.Departments.UpdateDepartment;
using CODWER.RERU.Core.DataTransferObjects.Departemnts;
using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : BaseController
    {
        public DepartmentController(IMediator mediator) : base(mediator) { }

        [HttpGet("{id}")]
        public async Task<DepartmentDto> GetDepartment([FromRoute] int id)
        {
            return await Mediator.Send(new GetDepartmentQuery { Id = id });
        }

        [HttpGet]
        public async Task<PaginatedModel<DepartmentDto>> GetDepartments([FromQuery] GetDepartmentsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddDepartment([FromBody] AddDepartmentCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpPatch]
        public async Task<int> EditDepartment([FromBody] UpdateDepartmentCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteDepartment([FromRoute] int id)
        {
            return await Mediator.Send(new RemoveDepartmentCommand { Id = id });
        }

        [HttpGet("select-values")]
        public async Task<List<SelectItem>> GetEvents()
        {
            var query = new GetDepartmentsValueQuery();

            return await Mediator.Send(query);
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintDepartmentsPdf([FromBody] PrintDepartmentCommand command)
        {
            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPut("excel-import")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> ImportFromExcelFile([FromForm] BulkExcelImport dto)
        {
            var command = new BulkImportDepartmentsCommand { Data = dto };

            var result = await Mediator.Send(command);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}
