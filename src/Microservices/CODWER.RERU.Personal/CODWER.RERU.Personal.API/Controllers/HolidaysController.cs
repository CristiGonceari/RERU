using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Holidays.AddHoliday;
using CODWER.RERU.Personal.Application.Holidays.GetHolidays;
using CODWER.RERU.Personal.Application.Holidays.RemoveHoliday;
using CODWER.RERU.Personal.Application.Holidays.UpdateHoliday;
using CODWER.RERU.Personal.DataTransferObjects.Holidays;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<HolidayDto>> GetHolidays([FromQuery] GetHolidaysQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }


        [HttpPost]
        public async Task<int> CreateHoliday([FromBody] AddEditHolidayDto dto)
        {
            var command = new AddHolidayCommand
            {
                Data = dto
            };
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateHoliday([FromBody] AddEditHolidayDto dto)
        {
            var command = new UpdateHolidayCommand
            {
                Data = dto
            };
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveHoliday([FromRoute] int id)
        {
            var command = new RemoveHolidayCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}
