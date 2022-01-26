using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.DismissalRequests.AddDismissalRequest;
using CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.MyRequests.GetDismissRequest;
using CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.SubordinateRequests.ApproveRejectRequest;
using CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.SubordinateRequests.GetRequests;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers.Profile
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestProfileController : BaseController
    {
        [HttpPost]
        public async Task<int> CreateDismissalRequest([FromBody] AddDismissalRequestCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedModel<MyDismissalRequestDto>> GetMyDismissalRequests([FromQuery] DismissalRequestQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPatch]
        public async Task<Unit> ApproveOrRejectRequestVacation([FromBody] ApproveRejectRequestDto dto)
        {
            var command = new ApproveRejectRequestCommand(dto);

            return await Mediator.Send(command);
        }

        [HttpGet("subordinate-requests")]
        public async Task<PaginatedModel<DismissalRequestDto>> GetDismissalRequests([FromQuery] GetSubordinateRequestsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}