using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.AddVacation;
using CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetAvailableDays;
using CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetVacationDaysInterval;
using CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetVacations;
using CODWER.RERU.Personal.Application.Profiles.Vacations.SubordinatesVacations.ApproveRejectVacation;
using CODWER.RERU.Personal.Application.Profiles.Vacations.SubordinatesVacations.GetVacations;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.API.Controllers.Profile
{
    [Route("api/profile/[controller]")]
    [ApiController]
    public class VacationProfileController : BaseController
    {
        [HttpPost]
        public async Task<int> CreateVacation([FromBody] AddMyVacationDto data)
        {
            var command = new AddVacationCommand(data);
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedModel<MyVacationDto>> GetMyVacations([FromQuery] GetVacationsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("available")]
        public async Task<double> GetAvailableContractorDays([FromQuery] GetAvailableDays query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("interval")]
        public async Task<double> GetIntervalVacationDays([FromQuery] GetVacationDaysIntervalQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPatch]
        public async Task<Unit> ApproveOrRejectSubordinateVacation([FromBody] ApproveRejectVacationDto dto)
        {
            var command = new ApproveRejectVacationCommand(dto);

            return await Mediator.Send(command);
        }

        [HttpGet("subordinate-vacations")]
        public async Task<PaginatedModel<SubordinateVacationDto>> GetSubordinateVacations([FromQuery] GetSubordinateVacationsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
