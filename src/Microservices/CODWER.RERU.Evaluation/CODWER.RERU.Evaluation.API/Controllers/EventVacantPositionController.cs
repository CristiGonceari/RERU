using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.EventVacantPositions.AddEditEventVacantPosition;
using CODWER.RERU.Evaluation.Application.EventVacantPositions.DeleteEventVacantPosition;
using CODWER.RERU.Evaluation.Application.EventVacantPositions.GetEventVacantPosition;
using CODWER.RERU.Evaluation.DataTransferObjects.EventVacantPositions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventVacantPositionController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<EventVacantPositionDto> GetRequiredDocumentPosition([FromRoute] int id)
        {
            var query = new GetEventVacantPositionQuery { Id = id };
            return await Mediator.Send(query);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteRequiredDocumentPosition([FromRoute] int id)
        {
            var command = new DeleteEventVacantPositionCommand { Id = id };
            return await Mediator.Send(command);
        }

        [HttpPost]
        public async Task<int> AddEditRequiredDocumentPosition([FromBody] AddEditEventVacantPositionCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
