using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.Positions.AddPosition;
using CODWER.RERU.Personal.Application.Positions.AddPreviousPosition;
using CODWER.RERU.Personal.Application.Positions.DismissFromPosition;
using CODWER.RERU.Personal.Application.Positions.GetCurrentContractorPosition;
using CODWER.RERU.Personal.Application.Positions.GetPosition;
using CODWER.RERU.Personal.Application.Positions.GetPositions;
using CODWER.RERU.Personal.Application.Positions.RemovePosition;
using CODWER.RERU.Personal.Application.Positions.TransferToNewPosition;
using CODWER.RERU.Personal.Application.Positions.UpdateCurrentContractorPosition;
using CODWER.RERU.Personal.Application.Positions.UpdatePosition;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<PositionDto>> GetPositions([FromQuery] GetPositionsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<PositionDto> GetPosition([FromRoute] int id)
        {
            var query = new GetPositionQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("current/{contractorId}")]
        public async Task<CurrentPositionDto> GetCurrentPosition([FromRoute] int contractorId)
        {
            var query = new GetCurrentContractorPositionQuery { ContractorId = contractorId };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreatePosition([FromBody] AddPositionCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdatePosition([FromBody] UpdatePositionCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch("current")]
        public async Task<Unit> UpdateCurrentPosition([FromBody] UpdateCurrentPositionCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch("dismiss-current")]
        public async Task<Unit> DismissCurrentPosition([FromBody] DismissFromPositionCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPost("transfer")]
        public async Task<Unit> TransferToNewPosition([FromBody] TransferToNewPositionCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPost("previous")]
        public async Task<int> AddPreviousPosition([FromBody] AddPreviousPositionCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemovePosition([FromRoute] int id)
        {
            var command = new RemovePositionCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}
