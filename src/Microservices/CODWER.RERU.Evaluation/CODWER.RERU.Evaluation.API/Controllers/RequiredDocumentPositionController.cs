using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.AddEditRequiredDocumentPosition;
using CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.DeleteRequiredDocumentPosition;
using CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.GetRequiredDocumentPosition;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequiredDocumentPositionController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<RequiredDocumentPositionDto> GetRequiredDocumentPosition ([FromRoute] int id)
        {
            var query = new GetRequiredDocumentPositionQuery { Id = id };
            return await Mediator.Send(query);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteRequiredDocumentPosition ([FromRoute] int id)
        {
            var command = new DeleteRequiredDocumentPositionCommand { Id = id };
            return await Mediator.Send(command);
        }

        [HttpPost]
        public async Task<int> AddEditRequiredDocumentPosition ([FromBody] AddEditRequiredDocumentPositionCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
