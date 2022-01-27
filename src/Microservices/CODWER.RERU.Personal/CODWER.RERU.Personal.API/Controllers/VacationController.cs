using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Vacations.AddContractorVacation;
using CODWER.RERU.Personal.Application.Vacations.GetAvailableDays;
using CODWER.RERU.Personal.Application.Vacations.GetContractorsVacations;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<VacationDto>> GetVacations([FromQuery] GetContractorVacationsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("available")]
        public async Task<double> GetContractorAvailableDays([FromQuery] GetAvailableDaysQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateVacation([FromBody] AddEditVacationDto dto)
        {
            var command = new AddVacationCommand(dto);

            return await Mediator.Send(command);
        }
        //[HttpGet]
        //public async Task<PaginatedModel<VacationDto>> GetVacations([FromQuery] GetMyVacationsQuery query)
        //{
        //    var result = await Mediator.Send(query);
        //    return result;
        //}

        //[HttpGet("file/{id}")]
        //[IgnoreResponseWrap]
        //public async Task<IActionResult> GetVacationFile([FromRoute] int id)
        //{
        //    var query = new GetVacationFileQuery { VacationId = id };
        //    var result = await Mediator.Send(query);

        //    return File(result.Content, result.ContentType, result.Name);
        //}

        //[HttpPatch]
        //public async Task<Unit> UpdateVacation([FromForm] AddEditVacationDto dto)
        //{
        //    var command = new UpdateVacationCommand(dto);

        //    var result = await Mediator.Send(command);

        //    return result;
        //}

        //[HttpPatch("file-update")]
        //public async Task<Unit> UpdateVacationFile([FromForm] UpdateVacationFileDto dto)
        //{
        //    var command = new UpdateVacationFileCommand(dto);

        //    var result = await Mediator.Send(command);

        //    return result;
        //}

        //[HttpDelete("{id}")]
        //public async Task<Unit> RemoveVacation([FromRoute] int id)
        //{
        //    var command = new RemoveVacationCommand { Id = id };
        //    var result = await Mediator.Send(command);

        //    return result;
        //}
    }
}
