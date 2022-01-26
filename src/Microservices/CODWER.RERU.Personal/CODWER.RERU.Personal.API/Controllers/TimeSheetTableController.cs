using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.TimeSheetTables.AddTimeSheetTableValue;
using CODWER.RERU.Personal.Application.TimeSheetTables.GetTimeSheetTableValues;
using CODWER.RERU.Personal.Application.TimeSheetTables.PrintAllTimeSheetTables;
using CODWER.RERU.Personal.Application.TimeSheetTables.PrintTimeSheetTables;
using CODWER.RERU.Personal.Application.TimeSheetTables.RemoveTimeSheetValues;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetTableController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<ContractorTimeSheetTableDto>> GetContractorsTimeSheetTableData([FromQuery] GetTimeSheetTableValuesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("export-excel")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetTimeSheetTableForExport([FromQuery] PrintTimeSheetTableCommand query)
        {
            var result = await Mediator.Send(query);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpGet("all-export-excel")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetAllTImeSheetTableForExport([FromQuery] PrintAllTimeSheetTableCommand query)
        {
            var result = await Mediator.Send(query);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpPost]
        public async Task<int> AddTimeSheetTableData([FromBody] AddEditTimeSheetTableCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("values")]
        public async Task<Unit> RemoveTimeSheetTableValues([FromQuery] RemoveTimeSheetTableCommand query )
        {
            var result = await Mediator.Send(query);

            return result;
        }

    }
}
